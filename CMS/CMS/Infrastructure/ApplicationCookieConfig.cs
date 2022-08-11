using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CMS.Infrastructure
{
    public static class ApplicationCookieConfig
    {
        public static void ConfigCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(config =>
            {
                config.ExpireTimeSpan = new TimeSpan(2, 0, 0);
                config.SlidingExpiration = true;
                config.Cookie.Name = "eSign.Cookie";
                config.LoginPath = "/auth";
                config.AccessDeniedPath = "/auth/accessdenied";
                config.Cookie.SameSite = SameSiteMode.Lax;
            });
        }
    }
}
