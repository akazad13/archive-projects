using TakeItToTheCloud.Models;
using TakeItToTheCloud.Models.Dto;

namespace TakeItToTheCloud.Services
{
    public interface IDashboardService
    {
        Task<IEnumerable<UploadedFileDetails>> GetUploadedFiles(int userid);
        Task<bool> AddUploadedFile(int userid, FileUploadDto files);
        Task<UploadedFile> GetUploadedFile(int consultationId);
        Task<bool> DeleteFile(int id);
    }
}