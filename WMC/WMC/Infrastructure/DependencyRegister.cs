using AutoMapper;
using WMC.DataAccess;
using WMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using WMC.Services;
using WMC.Repositories;
using WMC.Utilities;
using Amazon.SQS;

namespace WMC.Infrastructure
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
                options.AddPolicy("RequireStaffRole", policy => policy.RequireRole("Staff"));
                options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
                options.AddPolicy("RequireManagerOrStaffRole", policy => policy.RequireRole("Manager", "Staff"));
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
                config.Cookie.Name = "wmc.Cookie";
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
            services.AddTransient<IAWSSQSService, AWSSQSService>();

            //Repo
            services.AddScoped<IDashboardRepository, DashboardRepository>();

            //Helper
            services.AddTransient<IAWSS3Helper, AWSS3Helper>();    
            services.AddTransient<IAWSSQSHelper, AWSSQSHelper>();


            return services;
        }
    }
}
