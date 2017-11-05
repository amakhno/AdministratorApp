using AdministratorNegotiating.Models.Repositories;
using AdministratorNegotiating.Models.Repositories.Interfaces;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AdministratorNegotiating.Models
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MeetingsRepository>().As<IMeetingsRepository>();
            builder.RegisterType<UserManagerRepository>().As<IUserManagerRepository>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            // Set the dependency resolver for MVC.
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);

            // создаем новый контейнер с теми зависимостями, которые определены выше
            //container = builder.Build();

            // установка сопоставителя зависимостей
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}