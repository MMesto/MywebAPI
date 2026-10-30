using Microsoft.EntityFrameworkCore;
using MywebAPI.Data;
using MywebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// services toevoegen
builder.Services.AddControllers();
builder.Services.AddRouting();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext registreren
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDb")));

// Services registreren (altijd vóór builder.Build())
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IBlogpostService, BlogpostService>();

var app = builder.Build();

// middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
