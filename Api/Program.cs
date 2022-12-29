using System.Text;
using Application.IRepositories;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("https://0.0.0.0:5001", "http://0.0.0.0:5000");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCors(options =>
    {
        options
            .AddPolicy("CorsPolicy",
                policyBuilder =>
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
    });

var connectionString = builder.Configuration.GetConnectionString("MariaDBConnection");
var serverVersion = new MySqlServerVersion(new Version(5, 6, 24));

builder.Services.AddDbContext<AppDbContext>(dbContextOptions => dbContextOptions
    .UseMySql(connectionString, serverVersion)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .ConfigureWarnings(warnings => warnings
        .Ignore(CoreEventId.ContextInitialized))
);


var securityScheme = new OpenApiSecurityScheme
{
    Name = "New Auth",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer ",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security"
};

var securityReq = new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer "
            }
        },
        new string[] { }
    }
};

var info = new OpenApiInfo
{
    Version = "V1.0.0",
    Title = "New Rest API",
    Description = "New Rest API for New App"
};
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", info);
    options.AddSecurityDefinition("Bearer ", securityScheme);
    options.AddSecurityRequirement(securityReq);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddIdentity<Users, Roles>(o =>
    {
        o.Password.RequireDigit = true;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = true;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 8;
        o.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// builder.Services.AddAutoMapper(typeof(Program)); 
// builder.Services.AddControllersWithViews();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();