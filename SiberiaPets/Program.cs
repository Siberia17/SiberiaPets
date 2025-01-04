using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SiberiaPets.Infraestructure.Settings;
using SiberiaPets.Repositories;
using SiberiaPets.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Agrega la configuración de DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer("Bearer", options =>
//{
//    options.Authority = "https://localhost:44305";
//    options.Audience = "SiberiaPets";
//});


// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Authentication:JwtBearer:Authority"];
        options.Audience = builder.Configuration["Authentication:JwtBearer:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
