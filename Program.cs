var builder = WebApplication.CreateBuilder(args);

// ===== DEEL 1: services toevoegen (wat heeft je app nodig?) =====
builder.Services.AddControllers();          // zodat controllers werken
builder.Services.AddRouting();              // zodat URL's naar de juiste controller gaan
builder.Services.AddEndpointsApiExplorer(); // nodig voor Swagger
builder.Services.AddSwaggerGen();           // Swagger zelf

var app = builder.Build();

// ===== DEEL 2: middleware (welke route loopt een request af?) =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();    // maakt de beschrijving van je API
    app.UseSwaggerUI();  // maakt de testpagina
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();   // koppelt je controllers aan URL's

app.Run();