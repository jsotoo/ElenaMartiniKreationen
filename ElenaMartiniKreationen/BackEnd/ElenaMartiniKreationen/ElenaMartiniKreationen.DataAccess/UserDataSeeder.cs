using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.DataAccess
{
    public class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            // UserManager (Repositorio de Usuarios)
            var userManager = service.GetRequiredService<UserManager<IdentityUserElenaMartiniKreationen>>();
            // RoleManager (Repositorio de Roles)
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            // Crear roles
            var adminRole = new IdentityRole(Constants.RolAdmin);
            var clientRole = new IdentityRole(Constants.RolClient);

            await roleManager.CreateAsync(adminRole);

            await roleManager.CreateAsync(clientRole);

            // Usuario Administrador
            var adminUser = new IdentityUserElenaMartiniKreationen()
            {
                FullName = "Administrador del sistema",
                Address = "Av. Siempre viva 123",
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "paSSAdmin#132");
            if (result.Succeeded)
            {
                // Esto me asegura que el usuario se creo correctamente
                adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                if (adminUser is not null)
                    await userManager.AddToRoleAsync(adminUser, Constants.RolAdmin);
            }
        }


    }
}

