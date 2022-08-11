using WMC.Models;
using WMC.Models.Dto;

namespace WMC.Services
{
    public interface IDashboardService
    {
        Task<IEnumerable<Consultation>> GetConsultations(int userid);
        Task<bool> AddConsultation(int userid, AddConsultationDto consultation);
        Task<Consultation> GetConsultation(int consultationId);
        Task<bool> UpdateConsultation(UpdateConsultationDto consultation);
        Task<string> GetUploadedFileLink(int consultationId);
        Task<IEnumerable<string>> GetNotifications();
        Task<IEnumerable<Feedback>> GetFeedbacks(int userid);
        Task<bool> AddFeedback(int userid, AddFeedbackDto addFeedback);
        Task<IEnumerable<User>> GetStaffs();
        Task<bool> AddStaff(AddStaffDto staff);
    }
}