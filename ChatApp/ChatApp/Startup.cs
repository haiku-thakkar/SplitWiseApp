using ChatApp.Data;
using ChatApp.DataService;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp
{
    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    //        // In production, the Angular files will be served from this directory
    //        services.AddSpaStaticFiles(configuration =>
    //        {
    //            configuration.RootPath = "ClientApp/dist";
    //        });

    //        services.AddDbContext<ChatAppContext>(options =>
    //                options.UseSqlServer(Configuration.GetConnectionString("ChatAppContext")));


    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        else
    //        {
    //            app.UseExceptionHandler("/Error");
    //            app.UseHsts();
    //        }

    //        app.UseHttpsRedirection();
    //        app.UseStaticFiles();
    //        app.UseSpaStaticFiles();

    //        app.UseMvc(routes =>
    //        {
    //            routes.MapRoute(
    //                name: "default",
    //                template: "{controller}/{action=Index}/{id?}");
    //        });

    //        app.UseSpa(spa =>
    //        {
    //            // To learn more about options for serving an Angular SPA from ASP.NET Core,
    //            // see https://go.microsoft.com/fwlink/?linkid=864501

    //            spa.Options.SourcePath = "ClientApp";

    //            if (env.IsDevelopment())
    //            {
    //                spa.UseAngularCliServer(npmScript: "start");
    //            }
    //        });
    //    }
    //}
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public object GlobalHost { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChatAppContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("ChatAppContext")));
            services.AddScoped<LoginService, SQLLoginService>();
            services.AddScoped<MessageService, SQLMessageService>();
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });
            app.UseMvc();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("NOT FOUND !");
            });
        }
    }
}
