using AutoMapper;
using TakeItToTheCloud.Models;
using TakeItToTheCloud.Models.Dto;
using TakeItToTheCloud.Repositories;
using TakeItToTheCloud.Utilities;

namespace TakeItToTheCloud.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IMapper _mapper;
        private readonly IDashboardRepository _dashboardRepositoy;
        private readonly IAWSS3Helper _AWSS3Helper;
        private readonly IAccountService _accountService;


        public DashboardService(IMapper mapper,
            IDashboardRepository dashboardRepository,
            IAWSS3Helper AWSS3Helper,
            IAccountService accountService
        )
        {
            _mapper = mapper;
            _dashboardRepositoy = dashboardRepository;
            _AWSS3Helper = AWSS3Helper;
            _accountService = accountService;
        }
        public async Task<IEnumerable<UploadedFileDetails>> GetUploadedFiles(int userid)
        {
            if (userid != 0)
            {
                return _mapper.Map< IEnumerable<UploadedFile>, IEnumerable<UploadedFileDetails>>(await _dashboardRepositoy.GetUploadedFiles(userid));
            }
            else
            {
                return _mapper.Map<IEnumerable<UploadedFile>, IEnumerable<UploadedFileDetails>>(await _dashboardRepositoy.GetUploadedFiles());
            }
        }
        public async Task<UploadedFile> GetUploadedFile(int fileId)
        {
            return await _dashboardRepositoy.GetUploadedFile(fileId);
        }
        public async Task<bool> AddUploadedFile(int userid, FileUploadDto files)
        {
            try
            {
                if (files.Id > 0)
                {
                   var ret = await DeleteFile(files.Id);
                }
                var uploadedFileToAdd = new UploadedFile()
                {
                    Description = "",
                    Location = "",
                    UploadedTime = DateTime.Now,
                    UserId = userid
                };

                var filename = "";

                if (files.Doc.Files.Count > 0)
                {
                    filename = Guid.NewGuid().ToString() + files.Doc.Files[0].FileName;
                    uploadedFileToAdd.Description = files.Doc.Files[0].FileName;

                    using (var newMemoryStream = new MemoryStream())
                    {
                        files.Doc.Files[0].CopyTo(newMemoryStream);
                        uploadedFileToAdd.Location = await _AWSS3Helper.PushToAmazonS3ViaRest(filename, newMemoryStream);
                    }
                }


                _dashboardRepositoy.Add(uploadedFileToAdd);

                if (await _dashboardRepositoy.SaveAll())
                {
                    return true;
                }
                throw new Exception("Uploading files request failed on save.");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteFile(int id)
        {
            try
            {
                var filesFromDB = await _dashboardRepositoy.GetUploadedFile(id);
                var filename = filesFromDB.Location.Substring(filesFromDB.Location.LastIndexOf('/') + 1);

                var ret = await _AWSS3Helper.DeleteFileS3(filename);

                _dashboardRepositoy.Delete(filesFromDB);
                if (await _dashboardRepositoy.SaveAll())
                {
                    return true;
                }
                throw new Exception("Deleting the consultation request failed on save.");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public async Task<string> GetUploadedFileLink(int consultationId)
        //{
        //    var consultation = await _dashboardRepositoy.GetConsultation(consultationId);
        //    return _AWSS3Helper.GetFileURL(consultation.FileName);
        //}

    }
}
