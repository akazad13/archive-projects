using AutoMapper;
using WMC.Models;
using WMC.Models.Dto;
using WMC.Repositories;
using WMC.Utilities;

namespace WMC.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IMapper _mapper;
        private readonly IDashboardRepository _dashboardRepositoy;
        private readonly IAWSS3Helper _AWSS3Helper;
        private readonly IAWSSQSService _AWSSQSService;
        private readonly IAccountService _accountService;


        public DashboardService(IMapper mapper,
            IDashboardRepository dashboardRepository,
            IAWSS3Helper AWSS3Helper,
            IAWSSQSService AWSSQSService,
            IAccountService accountService
        )
        {
            _mapper = mapper;
            _dashboardRepositoy = dashboardRepository;
            _AWSS3Helper = AWSS3Helper;
            _AWSSQSService = AWSSQSService;
            _accountService = accountService;
        }
        public async Task<IEnumerable<Consultation>> GetConsultations(int userid)
        {
            if (userid != 0)
            {
                return await _dashboardRepositoy.GetAllConsultation(userid);
            }
            else
            {
                return await _dashboardRepositoy.GetAllConsultation();
            }
        }
        public async Task<Consultation> GetConsultation(int consultationId)
        {
            return await _dashboardRepositoy.GetConsultation(consultationId);
        }
        public async Task<bool> AddConsultation(int userid, AddConsultationDto consultation)
        {
            try
            {
                var consultationToAdd = _mapper.Map<Consultation>(consultation);
                consultationToAdd.UserId = userid;
                consultationToAdd.Status = "Pending";
                var filename = "";

                if (consultation.Doc.Files.Count > 0)
                {
                    filename = Guid.NewGuid().ToString() + Path.GetExtension(consultation.Doc.Files[0].FileName);
                    consultationToAdd.FileName = filename;
                }


                _dashboardRepositoy.Add(consultationToAdd);

                if (await _dashboardRepositoy.SaveAll())
                {
                    if (consultation.Doc.Files.Count > 0)
                    {
                        using (var newMemoryStream = new MemoryStream())
                        {
                            consultation.Doc.Files[0].CopyTo(newMemoryStream);
                            await _AWSS3Helper.PushToAmazonS3ViaRest(filename, newMemoryStream);
                        }
                    }

                    var message = "The Consultation request (" + consultationToAdd.Id + ") has been created.";
                    var isSent = await _AWSSQSService.PostMessageAsync(message);

                    return true;
                }
                throw new Exception("Creating the consultation request failed on save.");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateConsultation(UpdateConsultationDto consultation)
        {
            try
            {
                var consultatinoFromRepo = await _dashboardRepositoy.GetConsultation(consultation.Id);

                _mapper.Map(consultation, consultatinoFromRepo); // (from, to)

                if (await _dashboardRepositoy.SaveAll())
                {
                    var message = "The Consultation request (" + consultation.Id + ") has been " + consultation.Status + ".";
                    var isSent = await _AWSSQSService.PostMessageAsync(message);
                    return true;
                }
                throw new Exception("Updating the consultation request failed on save.");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> GetUploadedFileLink(int consultationId)
        {
            var consultation = await _dashboardRepositoy.GetConsultation(consultationId);
            return _AWSS3Helper.GetFileURL(consultation.FileName);
        }

        public async Task<IEnumerable<string>> GetNotifications()
        {
            var messages = (await _AWSSQSService.GetAllMessagesAsync())?.Select(m => m.Body);
            return messages;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacks(int userid)
        {
            if (userid != 0)
            {
                return await _dashboardRepositoy.GetFeedbacks(userid);
            }
            else
            {
                return await _dashboardRepositoy.GetFeedbacks();
            }
        }

        public async Task<bool> AddFeedback(int userid, AddFeedbackDto addFeedback)
        {
            try
            {
                var feedbackToAdd = _mapper.Map<Feedback>(addFeedback);
                feedbackToAdd.UserId = userid;


                _dashboardRepositoy.Add(feedbackToAdd);

                if (await _dashboardRepositoy.SaveAll())
                {
                    var user = await _accountService.GetUser(userid);
                    var message = user.Name + " added a new feedback.";
                    var isSent = await _AWSSQSService.PostMessageAsync(message);

                    return true;
                }
                throw new Exception("Posting the feedback failed on save.");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<User>> GetStaffs()
        {
            var staffs = await _dashboardRepositoy.GetStaffs();
            return staffs;
        }

        public async Task<bool> AddStaff(AddStaffDto staff)
        {
            try
            {
                var userForRegisterDto = _mapper.Map<UserForRegisterDto>(staff);
                return  await _accountService.Register(userForRegisterDto, "Staff");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
