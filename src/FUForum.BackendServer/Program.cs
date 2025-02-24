using FluentValidation.AspNetCore;
using FUForum.BackendServer.Data;
using FUForum.ViewModels.Systems;
using FUForum.BackendServer.Data.Entities;
using FUForum.BackendServer.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FUForum.BackendServer.IdentityServer;
using FUForum.BackendServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace FUForum.BackendServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Fluent validation
            builder.Services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<RoleCreateRequestValidator>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FUForum.BackendServer", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:7017/connect/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api.fuforum.access", "FUForum API" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new List<string> { "api.fuforum" }
                    }
                });
            });
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //ASP.NET Identity
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            });
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // IdentityServer4
            builder.Services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                }).AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.Ids)
                .AddAspNetIdentity<User>()
                .AddProfileService<IdentityProfileService>()
                .AddDeveloperSigningCredential();

            // Authen and Author
            builder.Services.AddAuthentication()
                .AddLocalApi("Bearer", option => { option.ExpectedScope = "api.fuforum.access"; });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                });
            });
            // Initialize data
            builder.Services.AddScoped<DbInitializer>();

            // SeriLog
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Services
            builder.Services.AddScoped<ISequenceService, SequenceService>();
            builder.Services.AddScoped<IStorageService, FileStorageService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FU Forum API V1");
                    c.OAuthClientId("swagger");
                });
            }

            app.UseIdentityServer();
            app.UseErrorWrapping();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

            await SeedData(); // Call seeding data method
            app.Run();

            // Seeding data method
            async Task SeedData()
            {
                using (var scope = app.Services.CreateScope())
                {
                    try
                    {
                        Log.Information("Seeding data...");
                        var dbInit = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                        await dbInit.Seed();
                    }
                    catch (Exception)
                    {
                        var logger = app.Services.GetRequiredService<ILogger<Program>>();
                        logger.LogError("An error occurred while seeding the database.");
                        throw;
                    }
                }
            }
        }
    }
}