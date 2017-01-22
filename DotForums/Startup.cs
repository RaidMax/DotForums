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
                        Title = "Administrators",
                    };

                    var Users = new Models.GroupModel
                    {
                        Title = "Users"
                    };

                    var General = new Models.CategoryModel
                    {
                        Title = "General",
                        Description = "Everything goes here!",
                    };

                    General.Permissions.Add(new Models.PermissionModel
                    {
                        Group = Administrators,
                        Permission = Models.PermissionModel.ALL_PERMISSIONS
                    });

                    var Administrator = new Models.UserModel
                    {
                        Username = "Administrator",
                        Email = "admin@dotforums.org",
                    };

                    Administrator.SetPassword("password");

                    var Thread = new Models.ThreadModel
                    {
                        Name = "Thread",
                        Title = "Thread Title",
                        Author = Administrator,
                    };

                    var Reply = new Models.PostModel
                    {
                        Name = "Reply",
                        Content = "Reply Content",
                        Author = Administrator,
                        Parent = Thread,
                    };

                    Thread.Permissions.Add(new Models.PermissionModel
                    {
                        Name = "Permissions",
                        Group = Administrators,
                        Permission = Models.PermissionModel.ALL_PERMISSIONS
                    });

                    Context.Categories.Add(General);
                    Context.Groups.Add(Administrators);
                    Context.Groups.Add(Users);

                    Administrator.Groups.Add(new Models.UserGroupModel
                    {
                        GroupID = Administrators.ID,
                        Group = Administrators,
                        UserID = Administrator.ID,
                        User = Administrator
                    });

                    var Skype = new Models.AttributeModel()
                    {
                        Title = new Models.AttributeTitleModel
                        {
                            Title = "Skype"
                        },
                        Value = "AdminSkypeAccount"
                    };

                    var Steam = new Models.AttributeModel()
                    {
                        Title = new Models.AttributeTitleModel()
                        {
                            Title = "Steam",
                            Link = "http://steamcommunity.com/id/{%LINK%}"
                        },
                        Value = "administrator"
                    };

                    var Avatar = new Models.FileModel()
                    {
                        Title = "Default Avatar",
                        FileName = "DefaultAvatar.png",
                        Type = Models.FileModel.FileType.IMAGE,
                        ContentType = "image/jpeg"
                    };

                    Context.Files.Add(Avatar);
                    Administrator.Profile.Attributes.Add(Skype);
                    Administrator.Profile.Attributes.Add(Steam);
                    Administrator.Profile.Avatar = Avatar;
                    Context.Users.Add(Administrator);
                    Thread.Posts.Add(Reply);
                    General.Threads.Add(Thread);

                    #region PERFORMANCE_TESTING
                    Models.UserModel User = null;
                    for (int i = 0; i < 100; i++)
                    {
                        User = new Models.UserModel
                        {
                            Username = "TestUser" + i,
                            Email = "user@dotforums.org" + i,
                        };

                        User.Groups.Add(new Models.UserGroupModel
                        {
                            User = User,
                            Group = Users
                        });

                        User.SetPassword("password");

                        User.Profile.Avatar = Avatar;    
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
