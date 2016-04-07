using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Module = Autofac.Module;

namespace Pixel.Sample.Host
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            // Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
        }
    }
}