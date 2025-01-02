using SiberiaPets.Repositories;
using SiberiaPets.Services;

var builder = WebApplication.CreateBuilder(args);

//Registra los servicios
builder.Services.AddTransient<IAnimalService, AnimalService>();
builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);   

// Add services to the container.
builder.Services.AddControllers();

//Obtiene la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("StringSql");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
