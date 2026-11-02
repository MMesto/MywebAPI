using Microsoft.EntityFrameworkCore;
using MywebAPI.Data;
using MywebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// s1: controllers + swagger
builder.Services.AddControllers();
builder.Services.AddRouting();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// s2: database, de connectionstring staat in appsettings.json
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDb")));

// s3: eigen services, scoped = één instantie per request (net als de DbContext)
// dit moet vóór builder.Build(), daarna kan je niks meer registreren
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IBlogpostService, BlogpostService>();

var app = builder.Build();

// m1: swagger alleen tijdens development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// m2: de rest van de pipeline, volgorde maakt hier uit
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
