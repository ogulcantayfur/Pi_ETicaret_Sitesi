using Microsoft.AspNetCore.Identity;
using Pi_ETicaret_Sitesi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi
{
    public class BaslangicGirisKimligi
    {
        public static void OlusturAdmin(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser
            {
                Name = "Ogulcan",
                SurName = "Tayfur",
                UserName = "Ogulcan"
            };
            if (userManager.FindByNameAsync("Ogulcan").Result == null)
            {
                //kullanicimi oluşturup şifresine de 1 verdik.
                var identityResult = userManager.CreateAsync(appUser, "1").Result;
            }

            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                var identityResult = roleManager.CreateAsync(role).Result;

                var result = userManager.AddToRoleAsync(appUser, role.Name).Result;
            }



        }

    }
}
