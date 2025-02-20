
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
        Description = "JWT Authorization header using bearer schem. enter Bearer [space] add your token in the text input. eg : Bearer srtert#$FEfsds",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
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
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    //options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
})/* For add new JWT again check Demo Controller
  .AddJwtBearer("JWTforGoogle", options =>
{
    //options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
})*/;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Use CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
