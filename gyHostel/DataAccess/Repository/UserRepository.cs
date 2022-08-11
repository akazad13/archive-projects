using DataAccess.Context;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
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

        public IEnumerable<User> Get()
        {
            return  _context.User.ToList();
        }

        public User Get(int id)
        {
            return _context.User.Find(id);
        }

        public User GetUserByAccount(string account)
        {
            return _context.User.Where(u => u.Account == account).FirstOrDefault();
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
