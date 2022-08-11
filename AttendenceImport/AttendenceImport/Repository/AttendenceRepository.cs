using AttendenceImport.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendenceImport.Repository
{
    public class AttendenceRepository : IAttendenceRepository
    {
        private readonly DataContext _context;
        public AttendenceRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void AddRange<T>(List<T> entity) where T : class
        {
            _context.AddRange(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
