using dotnet_api;
using dotnet_api.data;
using dotnet_api.Interfaces;
using dotnet_api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//I RELOCATE THIS IN THE StartupExtensions.cs
// builder.Services.AddControllers();
// builder.Services.AddTransient<Seed>();
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
// builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// builder.Services.AddScoped<ICountryRepository, CountryRepository>();
// builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
// builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
// builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    app.SeedData();

//RELOCATE THIS AS WELL IN THE StartupExtensions.cs
// if (args.Length == 1 && args[0].ToLower() == "seeddata")
//     SeedData(app);

// void SeedData(IHost app)
// {
//     var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

//     using (var scope = scopedFactory.CreateScope())
//     {
//         var service = scope.ServiceProvider.GetService<Seed>();
//         service.SeedDataContext();
//     }
// }


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
