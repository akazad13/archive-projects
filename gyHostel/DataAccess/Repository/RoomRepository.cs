using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
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

        public IEnumerable<Room> Get(string sort, string type)
        {
            var rooms = _context.Room.AsQueryable();
            type = type?.Trim()?.ToLower();
            if (!string.IsNullOrEmpty(type))
            {
                rooms = rooms.Where(r => r.Room_Type != null && r.Room_Type.ToLower() == type);
            }

            sort = sort?.Trim()?.ToLower();

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "desc")
                {
                    rooms = rooms.OrderByDescending(r => r.Fee);
                }
                else if (sort == "desc")
                {
                    rooms = rooms.OrderBy(r => r.Fee);
                }
            }
            return rooms.ToList();
        }

        public Room Get(int id)
        {
            return _context.Room.Find(id);
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
