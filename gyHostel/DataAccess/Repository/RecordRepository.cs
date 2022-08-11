using DataAccess.Context;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public RecordRepository(ApplicationDbContext context)
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

        public IEnumerable<Record> Get()
        {
            return _context.Record.ToList();
        }

        public Record Get(int id)
        {
            return _context.Record.Find(id);
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
