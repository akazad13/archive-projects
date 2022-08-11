using TakeItToTheCloud.Models;

namespace TakeItToTheCloud.Repositories
{
    public interface IDashboardRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(List<T> entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<UploadedFile>> GetUploadedFiles();
        Task<IEnumerable<UploadedFile>> GetUploadedFiles(int userid);
        Task<UploadedFile> GetUploadedFile(int id);
        Task<bool> SaveAll();
    }
}