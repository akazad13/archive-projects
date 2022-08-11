using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendenceImport.Repository
{
    public interface IAttendenceRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(List<T> entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}
