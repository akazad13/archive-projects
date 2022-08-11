using AttendenceImport.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendenceImport.DAL
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opts)
            : base(opts) { }

        public DbSet<ExcelData> ExcelData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
