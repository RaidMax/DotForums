using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DotForums
{
    public class Startup
    {
        private IHostingEnvironment CurrentEnvironment;
        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            if (CurrentEnvironment.IsDevelopment())
                services.AddMvc()
                    .AddJsonOptions(j => j.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented)
                    .AddJsonOptions(j => j.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            else
                services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "DotForums",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Error/Unauthorized"),
                AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Error/AccessDenied"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            using (var Context = new Models.ForumContext())
            {
                if (Context.Find<Models.UserModel>((ulong)1) == null)
                {
                    var Administrators = new Models.GroupModel
                    {
                        Name = "Administrators"
                    };

                    var General = new Models.CategoryModel
                    {
                        Name = "General",
                        Description = "Everything goes here!",
                    };

                    General.Permissions.Add(new Models.PermissionModel
                    {
                        Name = "Permissions",
                        Group = Administrators,
                        Permission = Models.PermissionModel.ALL_PERMISSIONS
                    });

                    var Administrator = new Models.UserModel
                    {
                        Name = "User",
                        Username = "Administrator",
                        Email = "admin@dotforums.org",
                        Group = Administrators
                    };

                    var Thread = new Models.ThreadModel
                    {
                        Name = "Thread",
                        Title = "Thread Title",
                        Content = "Thread Content",
                        Author = Administrator,
                    };

                    var Reply = new Models.ThreadModel
                    {
                        Name = "Reply",
                        Title = "Reply Title",
                        Content = "Reply Content",
                        Author = Administrator,
                        Parent = Thread
                    };

                    Thread.Permissions.Add(new Models.PermissionModel
                    {
                        Name = "Permissions",
                        Group = Administrators,
                        Permission = Models.PermissionModel.ALL_PERMISSIONS
                    });

                    Thread.Posts.Add(Reply);
                    General.Threads.Add(Thread);
                    Context.Categories.Add(General);
                    Context.Groups.Add(Administrators);
                    Context.Users.Add(Administrator);
                    Context.SaveChanges();


                    #region PERFORMANCE_TESTING
                    Models.UserModel User = null;
                    for (int i = 0; i < 0; i++)
                    {
                        User = new Models.UserModel
                        {
                            Name = "User",
                            Username = "Administrator" + i,
                            Email = "admin@dotforums.org" + i,
                            Group = Administrators
                        };
                        Context.Users.Add(User);
                    }

                    Context.SaveChanges();
                    #endregion
                }
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
