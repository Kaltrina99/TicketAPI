
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(ticketContext context, UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var users=new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName="Admin",
                        UserName="Admin",
                        Email="admin@test.com"
                    }
                };
                foreach(var user in users)
                {
                    await userManager.CreateAsync(user,"P@ssw0rd");
                }
            }

            if(!context.Companies.Any())
            {
                var companies= new List<Company>
                {
                    new Company
                    {
                        Name="Default",
                        Description="",
                        IsActive=true,
                        CreatedDate=DateTime.Today,
                        UpdatedDate=DateTime.Today,
                    }
                };
                context.Companies.AddRange(companies);
                context.SaveChanges();

            }
        }
    }
}