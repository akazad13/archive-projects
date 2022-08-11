using Microsoft.EntityFrameworkCore;
using TakeItToTheCloud.DataAccess;
using TakeItToTheCloud.Models;

namespace TakeItToTheCloud.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        protected readonly DataContext _dbContext;
        public DashboardRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _dbContext.AddAsync(entity);
        }

        public void AddRange<T>(List<T> entity) where T : class
        {
            _dbContext.AddRange(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public async Task<IEnumerable<UploadedFile>> GetUploadedFiles()
        {
            return await _dbContext.UploadedFiles.Include(uf => uf.User).ToListAsync();
        }

        public async Task<IEnumerable<UploadedFile>> GetUploadedFiles(int userid)
        {
            return await _dbContext.UploadedFiles.Include(uf => uf.User).Where(c => c.UserId == userid)?.ToListAsync();
        }

        public async Task<UploadedFile> GetUploadedFile(int id)
        {
            return await _dbContext.UploadedFiles.Include(uf => uf.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> SaveAll()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
