using DataAccess.Context;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    class FinanceRepository : IFinanceRepository
    {
        private readonly ApplicationDbContext _context;

        public FinanceRepository(ApplicationDbContext context)
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

        public IEnumerable<Finance> Get()
        {
            return _context.Finances.ToList();
        }

        public Finance Get(int id)
        {
            return _context.Finances.Find(id);
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
