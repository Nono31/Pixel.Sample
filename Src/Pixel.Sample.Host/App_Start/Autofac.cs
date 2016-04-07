using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Pixel.Sample.Business;
using Pixel.Sample.Data;

namespace Pixel.Sample.Host.App_Start
{

    internal class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //Logger
            //builder.RegisterModule<LoggerModule>();

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            // Register our Data dependencies
            builder.RegisterModule(new DataModule());

            //Register business module
            builder.RegisterModule<BusinessModule>();

            //Register web module
            builder.RegisterModule<WebModule>();

            //Build container
            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}