using Contracts;
using Entities;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace AspEventVieuwerAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            //services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IArtistGenreRepository, ArtistGenreRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IDatePlanningRepository, DatePlanningRepository>();
            services.AddScoped<IEventDateRepository, EventDateRepository>();
            services.AddScoped<IEventGenreRepository, EventGenreRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPreferenceRepository, PreferenceRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IScheduleItemRepository, ScheduleItemRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IStageRepository, StageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
