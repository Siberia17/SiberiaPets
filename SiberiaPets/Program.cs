using Microsoft.Extensions.Options;
using SiberiaPets.Infraestructure.Settings;
using SiberiaPets.Repositories;
using SiberiaPets.Services;

var builder = WebApplication.CreateBuilder(args);


//Agrega la configuración de DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

//Registra el servicio DatabaseSettings como un singleton
builder.Services.AddSingleton(provider =>
{
    return provider.GetService<IOptions<DatabaseSettings>>().Value; 
});

//Registra los servicios

builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();
builder.Services.AddTransient<IAnimalService, AnimalService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);   

// Add services to the container.
builder.Services.AddControllers();


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
