using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.Pl.Helper;
using Demo.Pl.Mapping_Profiles;
using Demo.Pl.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Pl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configure Services that allow dependancy injection
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MvcApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEmailSettings, EmailSetting>();

            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new RoleProfile()));

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();

            builder.Services.AddAuthentication();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

            })
                .AddEntityFrameworkStores<MvcApplicationDbContext>().
                AddDefaultTokenProviders();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "Account/Login";
                    option.AccessDeniedPath = "Home/Error";
                });





            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            #endregion


            var app = builder.Build();
            #region Configure HTTP requests pipelines
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion

            app.Run();
        }

    }
}




