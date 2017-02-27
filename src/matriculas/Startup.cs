using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Matriculas.Services;
using Microsoft.Extensions.Configuration;
using Matriculas.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Matriculas.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Matriculas.Queries.Core.Repositories;
using Matriculas.Queries.Persistence.Repositories;

namespace Matriculas
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json");

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
		    services.AddSingleton(_config);
		    if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
		    {
				services.AddScoped<IMailService, DebugMailService>();//Para habilitar el servicio de correo
		    }
		else
		{
			//Para implementar el servicio real
		}
		services.AddDbContext<MatriculasContext>();//Registra el contexto de las entidades
		services.AddIdentity<ApplicationUser, IdentityRole>(config =>
		{
			config.User.RequireUniqueEmail = true;
			config.Password.RequiredLength = 8;
			config.Password.RequireNonAlphanumeric = false;
			config.Password.RequireDigit = false;
			config.Password.RequireLowercase = false;
			config.Password.RequireUppercase = false;
			config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
			config.Cookies.ApplicationCookie.AccessDeniedPath = "/App/Index";
			config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
			{
				OnRedirectToLogin = async ctx =>
				{
					if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
					{
						ctx.Response.StatusCode = 401;
					}
					else
					{
						ctx.Response.Redirect(ctx.RedirectUri);
					}
					await Task.Yield();
				}
			};
			config.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		})
		.AddEntityFrameworkStores<MatriculasContext>()
		.AddDefaultTokenProviders();
		services.AddScoped <IAppRepository, AppRepository>();
		services.AddTransient<MatriculasContextSeedData>();
        services.AddLogging();
        services.AddMvc(config =>
        {
            if (_env.IsProduction())
            {
                config.Filters.Add(new RequireHttpsAttribute());
            } 
        })
        .AddJsonOptions(config => 
        {
            config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();  
            config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, 
        IHostingEnvironment env, 
        ILoggerFactory loggerFactory, 
        MatriculasContextSeedData seeder, 
        ILoggerFactory factory)
        {

            Mapper.Initialize(config =>
            {            
                config.CreateMap<CargoViewModel, Cargo>().ReverseMap();
                config.CreateMap<ColaboradorViewModel, Colaborador>().ReverseMap();
                config.CreateMap<GradoViewModel, Grado>().ReverseMap();
                config.CreateMap<SeccionViewModel, Seccion>().ReverseMap();
                config.CreateMap<NivelViewModel, Nivel>().ReverseMap();
                config.CreateMap<CursoViewModel, Curso>().ReverseMap();
                config.CreateMap<ProfesorViewModel, Profesor>().ReverseMap();
                config.CreateMap<AlumnoViewModel, Alumno>().ReverseMap();
                config.CreateMap<ApoderadoViewModel, Apoderado>().ReverseMap();
                config.CreateMap<CronogramaMatriculaViewModel, CronogramaMatricula>().ReverseMap();
                config.CreateMap<AnioAcademicoViewModel, AnioAcademico>().ReverseMap();
                config.CreateMap<MatriculaViewModel, Matricula>().ReverseMap(); 
                config.CreateMap<NotaViewModel, Nota>().ReverseMap();
                config.CreateMap<DeudaViewModel, Deuda>().ReverseMap();
            });

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                factory.AddDebug(LogLevel.Information);
            }
            else
            {
                factory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index"}
                );
            });

			seeder.EnsureSeedData().Wait();
        }
    }
}
