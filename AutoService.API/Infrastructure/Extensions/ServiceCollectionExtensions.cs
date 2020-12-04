namespace AutoService.API
{
    using System.Text;
    using AutoService.API.Features.Identity;
    using AutoService.API.Filters;
    using AutoService.API.Infrastructure.Services;
    using Catstagram.Server.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionExtensions
    {
        public static AppSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            var appSettings = applicationSettingsConfiguration.Get<AppSettings>();
            return appSettings;
        }


        //public static IServiceCollection AddIdentity(this IServiceCollection services)
        //{
        //    services
        //        .AddIdentity<User, IdentityRole>(options =>
        //        {
        //            options.Password.RequiredLength = 6;
        //            options.Password.RequireDigit = false;
        //            options.Password.RequireLowercase = false;
        //            options.Password.RequireNonAlphanumeric = false;
        //            options.Password.RequireUppercase = false;
        //        })
        //        .AddEntityFrameworkStores<CatstagramDbContext>();

        //    return services;
        //}

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<IConnectionFactory, ConnectionFactory>()
                .AddTransient<IIdentityService, IdentityService>();

            return services;
        }
           

        //public static IServiceCollection AddSwagger(this IServiceCollection services)
        //    => services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc(
        //            "v1",
        //            new OpenApiInfo
        //            {
        //                Title = "My Catstagram API",
        //                Version = "v1"
        //            });
        //    });

        public static void AddApiControllers(this IServiceCollection services)
            => services
                .AddControllers(options => options
                    .Filters
                    .Add<ModelOrNotFoundActionFilter>());
    }
}
