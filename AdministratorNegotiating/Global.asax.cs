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

            InitializeDatabase(new Models.ApplicationDbContext());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void InitializeDatabase(ApplicationDbContext context)
        {
            string login = "admin@admin.admin";
            string password = "12341234As.";
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (userManager.FindByName(login) == null)
            {
                var user = new ApplicationUser { UserName = login, Email = login };
                var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            }
            string nameOfAdministratorRole = "Administrators";
            userManager.AddToRole(userManager.FindByName(login).Id, nameOfAdministratorRole);
        }
    }
}
