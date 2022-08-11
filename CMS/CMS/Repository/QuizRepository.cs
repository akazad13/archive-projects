using CMS.DataAccess;
using CMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DataContext _context;
        public QuizRepository(DataContext context)
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

        public async Task<IEnumerable<Quiz>> GetAll()
        {
            return await _context.Quizzes.Where(c => c.Id != 1 && c.Id != 2).ToListAsync();
        }


        public async Task<IEnumerable<Quiz>> GetAll(int userid)
        {
            return await _context.Quizzes.Where(c => c.UserId == userid && (c.Id != 1 && c.Id != 2)).ToListAsync();
        }

        public async Task<Quiz> GetOne(int id)
        {
            return await _context.Quizzes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<QAndA>> GetQuestions(int quizId)
        {
            return await _context.QAndAs.Where(q => q.QuizId == quizId).ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetAllEvaluations()
        {
            return await _context.EvaluationResults.Select(e => new
            {
                e.UserId,
                CourseName = e.Quizzes.Course.Name,
                QuizId = e.QuizId,
                Score = (e.CorrectAnswer*100)/((double)e.TotalQuestions * 1.0)
            }).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
