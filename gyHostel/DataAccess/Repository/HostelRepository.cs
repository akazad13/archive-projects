using DataAccess.Context;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    class HostelRepository : IHostelRepository
    {
        private readonly ApplicationDbContext _context;

        public HostelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public IEnumerable<Hostel> Get()
        {
            return _context.Hostel.ToList();
        }

        public Hostel Get(int id)
        {
            return _context.Hostel.Find(id);
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
