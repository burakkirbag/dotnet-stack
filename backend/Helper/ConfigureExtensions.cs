using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using stack.Data;
using stack.Helper.Middlewares;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace stack.Helper
{
    public static class ConfigureExtensions
    {
        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            //app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<UnauthorizedMiddleware>();
            app.UseMiddleware<ForbiddenMiddleware>();
        }

        public static void ConfigureDatabaseContext(this IServiceCollection services)
        {
            services.AddDbContext<StackDbContext>(opt => opt.UseNpgsql(StackEnvironments.ConnectionString));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, UserRole>()
                           .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                           .AddEntityFrameworkStores<StackDbContext>()
                           .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var tcs = new TaskCompletionSource<object>();

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = StackEnvironments.JwtAuthOptions.Issuer,
                    ValidAudience = StackEnvironments.JwtAuthOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StackEnvironments.JwtAuthOptions.SigningKey)),
                    RoleClaimType = "Roles"
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            var bearerToken = context.Request.Headers["Authorization"].ToString();
                            if (bearerToken.StartsWith("Bearer"))
                                bearerToken = bearerToken.Remove(0, "Bearer".Length);

                            context.Token = bearerToken.Trim();
                        }
                        else
                        {
                            context.Token = context.Request.Cookies[StackEnvironments.CookieAuthOptions.Name];
                        }

                        await tcs.Task;
                    }
                };

                tcs.TrySetResult(true);
            });
        }

        public static void ConfigureSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {Token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Stack Web API",
                    Version = "v1",
                    Description = "Stack Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Burak Kırbağ",
                        Email = "burak@burakkirbag.com",
                        Url = new Uri("http://www.burakkirbag.com")
                    }
                });
            });
        }
    }
}
