namespace Backend
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Http;
    using Microsoft.Framework.DependencyInjection;
    using System;

    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();

            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");

            //app.UseRuntimeInfoPage();

            //app.UseWelcomePage("/welcome");

            /*app.Run(async (context) =>
            {
                if (context.Request.Query.ContainsKey("throw")) throw new Exception("Exception triggered!");
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>Hello World!");
                await context.Response.WriteAsync("<ul>");
                await context.Response.WriteAsync("<li><a href=\"/welcome\">Welcome Page</a></li>");
                await context.Response.WriteAsync("<li><a href=\"/?throw=true\">Throw Exception</a></li>");
                await context.Response.WriteAsync("</ul>");
                await context.Response.WriteAsync("</body></html>");
            });*/

            //ConfigureAuth(app);
        }
    }
}