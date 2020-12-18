using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityManagement.Applications.Users.AddNewUser;
using IdentityManagement.Filters;
using IdentityManagement.Helpers;
using IdentityManagement.Infrastructure.DbContexts;
using IdentityManagement.Infrastructure.Migrations;
using IdentityManagement.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IdentityManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });

            services.AddApiVersioning(apiVerConfig =>
            {
                apiVerConfig.AssumeDefaultVersionWhenUnspecified = true;
                apiVerConfig.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddEntityFrameworkNpgsql().AddDbContext<IdentityManagementContext>((sp, options) =>
            {
                options.UseNpgsql(Configuration.GetConnectionString(name: "IdentityManagementDB"));
                options.UseInternalServiceProvider(sp);
            });

            services.AddLogging(c => c.AddFluentMigratorConsole())
                    .AddFluentMigratorCore()
                    .ConfigureRunner(c => c
                    .AddPostgres()
                    .WithGlobalConnectionString("IdentityManagementDB")
                   .ScanIn(typeof(InitialTableCreation_202012180613).Assembly).For.Migrations());

            AddCustomSwagger(services);

            services.AddMediatR(typeof(Startup));

            services.AddTransient<IRepository, EfRepository>();
            services.AddTransient<IValidator<AddNewUserCommand>, AddNewUserValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            CreateNewDatabase.EnsureDatabase(Configuration.GetConnectionString(name: "IdentityManagementDBMigration"), AppSettings.MigrationDatabase);
            migrationRunner.MigrateUp();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger()
               .UseSwaggerUI(setup =>
               {
                   setup.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "IdentityAccessManagement.API V1");
               });
        }
        private void AddCustomSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Identity And Access Management HTTPS Services",
                    Version = "v1",
                    Description = "The Effism NexGen's Identity And Access Management Services are used to create and manage all types of users."
                });
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
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
                            new string[] {}
                    }
                });
            });
        }
    }
}
