namespace AdministratorNegotiating.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Security;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<AdministratorNegotiating.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdministratorNegotiating.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            /*if (context.Roles.Where(x => x.Name == nameOfAdministratorRole).Count() == 0)
            {
                Roles.CreateRole(nameOfAdministratorRole);
            }
            string nameOfAdministratorRole = "Administrators";
            string login = "admin@admin.com";
            string password = "1234";
            /*if (context.Users.Where(x => x.UserName == userName).Count() == 0)
            {
                PasswordHasher hasher = new PasswordHasher();
                string passwordHash = hasher.HashPassword(userPassword);
                var user = new ApplicationUser { UserName = userName, Email = userName, PasswordHash = passwordHash };
                context.Users.Add(user);
                context.SaveChanges();
            }
            Roles.AddUserToRole(userName, nameOfAdministratorRole);

            var administratorRole = context.Roles.First(x => x.Name == nameOfAdministratorRole);
            if (administratorRole == null)
            {
                administratorRole = new IdentityRole(nameOfAdministratorRole);
                context.Roles.Add(administratorRole);                
            }
            var administratorUser = context.Users.First(x => x.UserName == login);
            if (administratorUser == null)
            {
                PasswordHasher hasher = new PasswordHasher();
                string passwordHash = hasher.HashPassword(password);
                administratorUser = new ApplicationUser { UserName = login, Email = login, PasswordHash = passwordHash };
                context.Users.Add(administratorUser);
            }
            IdentityUserRole a = new IdentityUserRole();
            a.RoleId = administratorRole.Id;
            a.UserId = administratorUser.Id;
           
            if (! context.Roles.First(x => x.Name == nameOfAdministratorRole).Users.Contains(a))
            {
                context.Roles.First(x=>x.Id == a.RoleId).Users.Add(a);
            }
            context.SaveChanges();*/
        }
    }
}
