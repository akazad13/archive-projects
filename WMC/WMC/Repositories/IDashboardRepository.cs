using WMC.Models;

namespace WMC.Repositories
{
    public interface IDashboardRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(List<T> entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Consultation>> GetAllConsultation();
        Task<IEnumerable<Consultation>> GetAllConsultation(int userid);
        Task<Consultation> GetConsultation(int id);
        Task<IEnumerable<Feedback>> GetFeedbacks();
        Task<IEnumerable<Feedback>> GetFeedbacks(int userid);
        Task<IEnumerable<User>> GetStaffs();
        Task<bool> SaveAll();
    }
}