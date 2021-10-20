using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Common
{
    public static class IdentityDataInitializer
    {
        public static void SeedRolesAndUser
        (UserManager<User> userMenager, RoleManager<IdentityRole<Guid>> roleMenager, IConfiguration configuration)
        {
            SeedRoles(roleMenager);
            SeedUser(userMenager, configuration);
        }

        public static void SeedUser(UserManager<User> userManager, IConfiguration configuration)
        {
            var userExists = userManager.FindByNameAsync(configuration["FirstUser:Username"]).Result != null;
            if (!userExists)
            {
                User user = new()
                {
                    UserName = configuration["FirstUser:Username"],
                    Email = configuration["FirstUser:Email"],
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                IdentityResult result = userManager.CreateAsync(user, configuration["FirstUser:Password"]).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRoles.User).Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<IdentityRole<Guid>> roleMenager)
        {
            var type = typeof(UserRoles);
            var fields = type.GetFields();
            List<string> fieldNames = new(type.GetFields().Select(x => x.Name));

            for (int i = 0; i < fields.Length; i++)
            {
                if (!roleMenager.RoleExistsAsync(fields[i].GetValue(type).ToString()).Result)
                    roleMenager.CreateAsync(new IdentityRole<Guid>(fields[i].GetValue(type).ToString())).Wait();
            }
        }
    }
}
