using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineDiscussion.Data;
namespace OnlineDiscussion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("OnlineDiscussionContextConnection") ?? throw new InvalidOperationException("Connection string 'OnlineDiscussionContextConnection' not found.");

                                    builder.Services.AddDbContext<OnlineDiscussionContext>(options =>
                options.UseSqlServer(connectionString));

                                                builder.Services.AddDefaultIdentity<Areas.Identity.Data.OnlineDiscussionUser>()
                .AddEntityFrameworkStores<OnlineDiscussionContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();
            app.MapRazorPages();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}