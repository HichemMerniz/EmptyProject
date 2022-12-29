using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services
            .AddCors(options =>
            {
                options
                    .AddPolicy("CorsPolicy",
                        builder =>
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
            });
    }

    public static void ConfigureIISIntegration(
        this IServiceCollection services
    )
    {
        services
            .Configure<IISOptions>(options => { });
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        // services
        //     .AddApiVersioning(opt =>
        //     {
        //       opt.ReportApiVersions = true;
        //       opt.AssumeDefaultVersionWhenUnspecified = true;
        //       opt.DefaultApiVersion = new ApiVersion(1, 0);
        //     });
    }

    // public static void ConfigureLoggerService(
    //     this IServiceCollection services
    // ) => services.AddScoped<ILoggerManager, LoggerManager>();

    public static void ConfigureMySqlContext(
        this IServiceCollection services,
        IConfiguration Configuration,
        string ApiProjectName
    )
    {
        var connectionString = Configuration.GetConnectionString("MariaDBConnection");
        var serverVersion = new MySqlServerVersion(new Version(10, 6, 5));
        services.AddDbContext<AppDbContext>
        (
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion,
                    mySqlOptions => mySqlOptions.MigrationsAssembly("WebApplication"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
    }
    //
    // public static void ConfigureSqlite(
    //     this IServiceCollection services,
    //     IConfiguration Configuration,
    //     string ApiProjectName
    // )
    // {
    //   services
    //       .AddDbContext<AppDbContext>(dbContextOptions =>
    //           dbContextOptions
    //               .UseSqlite(Configuration.GetConnectionString("SqliteConnection"),
    //               o => o.MigrationsAssembly(ApiProjectName))
    //               .UseLoggerFactory(LoggerFactory
    //                   .Create(logging =>
    //                       logging
    //                           .AddConsole()
    //                           .AddFilter(level =>
    //                               level >= LogLevel.Information)))
    //               .EnableSensitiveDataLogging()
    //               .EnableDetailedErrors());
    // }

    // public static void ConfigureRepositoryWrapper(
    //     this IServiceCollection services
    // ) => services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    //
    // public static void ConfigureActionFilters(this IServiceCollection services)
    // {
    //   services.AddScoped<ValidationFilterAttribute>();
    // }

    // public static void ConfigureRateLimitingOptions(
    //     this IServiceCollection services
    // )
    // {
    //   var rateLimitRules =
    //       new List<RateLimitRule> {
    //               new RateLimitRule { Endpoint = "*", Limit = 60, Period = "1m" }
    //       };
    //   services
    //       .Configure<IpRateLimitOptions>(opt =>
    //       {
    //         opt.GeneralRules = rateLimitRules;
    //       });
    //   services
    //       .AddSingleton
    //       <IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    //   services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    //   services
    //       .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    //   services
    //       .AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(
    //       );
    // }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder =
            services
                .AddIdentity<Users, Roles>(o =>
                {
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 10;
                    o.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(
        this IServiceCollection services,
        IConfiguration Configuration
    )
    {
        var jwtSettings = Configuration.GetSection("JwtSettings");
        // services
        //     .AddAuthentication(opt =>
        //     {
        //       opt.DefaultAuthenticateScheme =
        //               JwtBearerDefaults.AuthenticationScheme;
        //       opt.DefaultChallengeScheme =
        //               JwtBearerDefaults.AuthenticationScheme;
        //     })
        //     .AddJwtBearer(options =>
        //     {
        //       options.TokenValidationParameters =
        //               new TokenValidationParameters
        //               {
        //                 ValidateIssuer = true,
        //                 ValidateAudience = true,
        //                 ValidateLifetime = false,
        //                 ValidateIssuerSigningKey = true,
        //                 ValidIssuer =
        //                       jwtSettings.GetSection("validIssuer").Value,
        //                 ValidAudience =
        //                       jwtSettings.GetSection("validAudience").Value,
        //                 IssuerSigningKey =
        //                       new SymmetricSecurityKey(Encoding
        //                               .UTF8
        //                               .GetBytes(jwtSettings
        //                                   .GetSection("SECRET")
        //                                   .Value))
        //               };
        //     });
    }

    //Not delete
    // public static void ConfigureClaimsPolicies(this IServiceCollection services)
    // {
    //   services
    //       .AddAuthorization(options =>
    //         {
    //           foreach (var permission in Enum.GetNames(typeof(Permissions)))
    //           {
    //             options.AddPolicy(permission, policy => policy.RequireClaim(permission));
    //           }
    //         });
    // }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services
            .AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Empty API", Version = "v1" });
                s.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Place to add JWT with Bearer",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =
                                new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
    }
}