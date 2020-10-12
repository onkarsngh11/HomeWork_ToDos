using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CorrelationId;
using AutoMapper;
using System.Security.Claims;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HomeWork_ToDos.CommonLib.Constants;
using HomeWork_ToDos.API.Services;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.DAL;
using HomeWork_ToDos.BL;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.Middlewares;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using HomeWork_ToDos.DAL.DbContexts;
using HomeWork_ToDos.API.Middlewares;
using HomeWork_ToDos.CommonLib.Helpers;

namespace HomeWork_ToDos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString(CommonConstants.SqlConnectionString)));
            services.Configure<AppSettings>(Configuration.GetSection(CommonConstants.AppSettings));
            services.AddAutoMapper(c => c.AddProfile<AutoMappingProfile>(), typeof(Startup));

            //configure services for checking, logging and forwarding correlationID.
            services.AddCorrelationIdHandlerAndDefaults();

            services.AddControllers(p => p.RespectBrowserAcceptHeader = true).AddXmlDataContractSerializerFormatters();
            services.AddHttpContextAccessor();
            services.AddApiVersioning(x =>
                    {
                        x.DefaultApiVersion = new ApiVersion(1, 0);
                        x.AssumeDefaultVersionWhenUnspecified = true;
                        x.ReportApiVersions = true;
                    });
            //Configure InApp services
            services.AddTransient<IUserContract, UserService>();
            services.AddTransient<IUserDbOps, UserDbOps>();
            services.AddTransient<IToDoListContract, ToDoListService>();
            services.AddTransient<IToDoListDbOps, ToDoListDbOps>();
            services.AddTransient<ILabelContract, LabelService>();
            services.AddTransient<ILabelDbOps, LabelDbOps>();
            services.AddTransient<IToDoItemContract, ToDoItemService>();
            services.AddTransient<IToDoItemDbOps, ToDoItemDbOps>();

            services.AddJwtAuthentication(Configuration);
            services.AddAuthorization(config =>
            {
                config.AddPolicy("Admin",
                    policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "Admin" }));
                config.AddPolicy("User",
                    policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "User" }));
            });

            //Configure Swagger services.
            services.AddSwagger();

            services.AddCors(options =>
            {
                options.AddPolicy(CommonConstants.AllowAllCors,
                                  builder =>
                                  {
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyOrigin();
                                  });
            });
            services.AddGraphQLServices();
            services.AddControllersWithViews(options =>
                    {
                        options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
                    })
                    .AddNewtonsoftJson();
        }
        //This method gets NewtonsoftJsonPatchInputFormatter for formatting JsonPatch document input.
        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            ServiceProvider builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(CommonConstants.LogFile, isJson: true);
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                ToDoDbContext toDoContext = serviceScope.ServiceProvider.GetRequiredService<ToDoDbContext>();
                toDoContext.Database.Migrate();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationId();
            app.UseContentLocationMiddleware();
            app.UseRequestResponseLogging();
            app.ConfigureExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseJwtMiddleware();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseGraphQL()
                .UsePlayground();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeWork ToDos API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
