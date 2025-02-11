using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TwitterCloneApp.Contexts;
using TwitterCloneApp.Data.Repositories.PostRepository;
using TwitterCloneApp.Data.Repositories.UserRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddDbContext<TwitterCloneEFContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlConnection"), b => b.MigrationsAssembly("TwitterCloneApp")));
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


var app = builder.Build();

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
