using AutoMapper;
using TakeItToTheCloud.DataAccess;
using TakeItToTheCloud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TakeItToTheCloud.Services;
using TakeItToTheCloud.Repositories;
using TakeItToTheCloud.Utilities;
using Amazon.SQS;

namespace TakeItToTheCloud.Infrastructure
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHttpContextAccessor();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireuserRole", policy => policy.RequireRole("User"));
            });

            services.AddControllersWithViews(options =>
            {
                //  for adding authentication check
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );

            services.ConfigureApplicationCookie(config =>
            {
                config.ExpireTimeSpan = new TimeSpan(2, 0, 0);
                config.SlidingExpiration = true;
                config.Cookie.Name = "TakeItToTheCloud.Cookie";
                config.LoginPath = "/account/login";
                config.AccessDeniedPath = "/account/accessdenied";
                config.Cookie.SameSite = SameSiteMode.Lax;
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            });


            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddDefaultTokenProviders();

            // Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDashboardService, DashboardService>();

            //Repo
            services.AddScoped<IDashboardRepository, DashboardRepository>();

            //Helper
            services.AddTransient<IAWSS3Helper, AWSS3Helper>();    


            return services;
        }
    }
}
