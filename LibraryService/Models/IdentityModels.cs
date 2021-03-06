﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryService.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("UserDbConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new IdentityDbInitialiser());
            Database.Initialize(true);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class IdentityDbInitialiser : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
                IdentityRole role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Staff"))
            {
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
                IdentityRole role = new IdentityRole { Name = "Staff" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
                IdentityRole role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    EmailConfirmed = true,
                };

                manager.Create(user, "Admin123");
                manager.AddToRole(user.Id, "Admin");
                manager.AddToRole(user.Id, "User");
                manager.AddToRole(user.Id, "Staff");
            }

            if (!context.Users.Any(u => u.UserName == "Staff"))
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "staff@test.com",
                    Email = "staff@test.com",
                    EmailConfirmed = true,
                };

                manager.Create(user, "Staff123");
                manager.AddToRole(user.Id, "Staff");
                manager.AddToRole(user.Id, "User");
            }

            if (!context.Users.Any(u => u.UserName == "User"))
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "user@test.com",
                    Email = "user@test.com",
                    EmailConfirmed = true,
                };

                manager.Create(user, "user123");
                manager.AddToRole(user.Id, "User");
            }

        }
    }
}


