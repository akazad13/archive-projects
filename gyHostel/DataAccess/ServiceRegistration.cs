using DataAccess.Context;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddDatabaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IFinanceRepository, FinanceRepository>();
            #endregion
        }
    }
}
