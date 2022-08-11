using DataAccess.Context;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
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

        public IEnumerable<Review> Get()
        {
            return _context.Review.ToList();
        }

        public Review Get(int id)
        {
            return _context.Review.Find(id);
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
