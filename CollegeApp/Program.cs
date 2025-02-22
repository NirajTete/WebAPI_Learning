
using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI_Learning.Config;
using WebAPI_Learning.Data;
using WebAPI_Learning.Repository.Implementation;
using WebAPI_Learning.Repository.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

builder.Logging.AddLog4Net();

#region Serilog Settings
//Log.Logger = new LoggerConfiguration().
//    MinimumLevel.Information()
//    .WriteTo.File("Log/log.txt",
//    rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

//use this line to override the built-in loggers
//builder.Host.UseSerilog();

//Use serilog alogn with built-in loggers
//builder.Logging.AddSerilog();
#endregion

// sql server db connection 
builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
});

// Add services to the container.
builder.Services.AddControllers(
//options => options.ReturnHttpNotAcceptable = true
).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//To allow Authorization in swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter your JWT token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});



builder.Services.AddTransient<IMyLogger, LogToServerMemory>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));

//Auto Mapper service register
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//To Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

    /* options.AddPolicy("AllowAll", policy =>
     {
         ////Allowing All Origins
         policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

         ////Allowing only few origin
         //policy.WithOrigins("https://localhost:4200");
         //policy.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
     });*/

    options.AddPolicy("AllowOnlyLocalhost", policy =>
    {
        ////Allowing only few origin
        //policy.WithOrigins("https://localhost:4200");
        policy.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });

    //Check in Demo Controller 
    options.AddPolicy("AllowOnlyGoogle", policy =>
    {
        ////Allowing only few origin
        //policy.WithOrigins("https://localhost:4200");
        policy.WithOrigins("https://google.com", "https://gmail.com", "http://googledrive.com").AllowAnyHeader().AllowAnyMethod();
    });
});

//JWT Authentication Configuration
var jwtSecret = builder.Configuration["JWTSecret"];
var key = Encoding.UTF8.GetBytes(jwtSecret);
var issuer = builder.Configuration["Issuer"]; // Ensure key name matches token generation
var audience = builder.Configuration["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });
/* For add new "Named" JWT again check Demo Controller
      .AddJwtBearer("JWTforGoogle", options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    })*/
;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TO fix the url
app.Urls.Add("https://0.0.0.0:7185");

app.UseHttpsRedirection();

//Use CORS
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
