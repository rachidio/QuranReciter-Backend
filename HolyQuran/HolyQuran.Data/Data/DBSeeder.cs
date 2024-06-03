using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HolyQuran.Data.Models;
using System.Collections.Generic;
using HolyQuran.Core.Enums;

namespace HolyQuran.Data.Data
{
    public static class DBSeeder
    {
        public static IHost SeedDb(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    context.Database.Migrate();
                    SeedTajweedRule(context);

                    var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    RoleManager.SeedRoles().Wait();

                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    //throw;
                }
            }
            return webHost;
        }

        public static async Task SeedRoles(this RoleManager<IdentityRole> RoleManager)
        {

            if (!RoleManager.Roles.Any())
            {

                var roles = new List<string>();
                roles.Add(UserType.Admin.ToString());
                roles.Add(UserType.Student.ToString());
                roles.Add(UserType.Teacher.ToString());

                foreach (var role in roles)
                {
                    await RoleManager.CreateAsync(new IdentityRole(role));
                }

            }

        }

        public static void SeedTajweedRule(this ApplicationDbContext context)
        {
            if (context.TajweedRules.Any())
            {
                return;
            }
            context.TajweedRules.AddRange(new TajweedRule() { NameArabic = "الاقلاب", NameLatin = "الاقلاب" });
            context.SaveChanges();
        }
    }
    }
