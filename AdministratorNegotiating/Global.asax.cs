using AdministratorNegotiating.Migrations;
using AdministratorNegotiating.Models;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace AdministratorNegotiating
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AdministratorNegotiating.Models.AutofacConfig.ConfigureContainer();

            //InitializeDatabase(new Models.ApplicationDbContext());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void InitializeDatabase(ApplicationDbContext context)
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

            */
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
            Roles.AddUserToRole(userName, nameOfAdministratorRole);*/

            var administratorRoles = context.Roles.Where(x => x.Name == nameOfAdministratorRole);       
            IdentityRole administratorRole;
            if (administratorRoles.Count() == 0)
            {
                administratorRole = new IdentityRole(nameOfAdministratorRole);
                context.Roles.Add(administratorRole);
                context.SaveChanges();
            }
            else
            {
                administratorRole = administratorRoles.First();
            }
            var administratorUsers = context.Users.Where(_=>_.UserName == login);

            ApplicationUser administratorUser;
            if (administratorUsers.Count() == 0)
            {
                PasswordHasher hasher = new PasswordHasher();
                string passwordHash = hasher.HashPassword(password);
                administratorUser = new ApplicationUser { UserName = login, Email = login, PasswordHash = passwordHash };
                context.Users.Add(administratorUser);
                context.SaveChanges();
            }
            else
            {
                administratorUser = administratorUsers.First();
            }

            //Init new Role
            IdentityUserRole a = new IdentityUserRole();
            a.RoleId = administratorRole.Id;
            a.UserId = administratorUser.Id;

            var administratorUserRoles = context.Roles.Where(x => x.Name == nameOfAdministratorRole);
            if (! context.Roles.First(x => x.Name == nameOfAdministratorRole).Users.Contains(a))
            {
                context.Roles.First(x => x.Name == nameOfAdministratorRole).Users.Add(a);
                context.SaveChanges();
            }
        }
    }
}
