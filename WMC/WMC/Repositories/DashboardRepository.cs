using Microsoft.EntityFrameworkCore;
using WMC.DataAccess;
using WMC.Models;

namespace WMC.Repositories
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

        public async Task<IEnumerable<Consultation>> GetAllConsultation()
        {
            return await _dbContext.Consultations.ToListAsync();
        }

        public async Task<IEnumerable<Consultation>> GetAllConsultation(int userid)
        {
            return await _dbContext.Consultations.Where(c => c.UserId == userid)?.ToListAsync();
        }

        public async Task<Consultation> GetConsultation(int id)
        {
            return await _dbContext.Consultations.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacks()
        {
            return await _dbContext.Feedbacks.ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacks(int userid)
        {
            return await _dbContext.Feedbacks.Where(c => c.UserId == userid)?.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetStaffs()
        {
            return await _dbContext.Users.Where(u=> u.UserRoles.Any(ur=> ur.Role.Name == "Staff"))?.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
