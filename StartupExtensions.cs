using dotnet_api.Interfaces;
using dotnet_api.Repository;

namespace dotnet_api
{
    public static class StartupExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<Seed>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add your repository registrations here
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewerRepository, ReviewerRepository>();
        }

        public static void SeedData(this IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<Seed>();
                service.SeedDataContext();
            }
        }
    }
}