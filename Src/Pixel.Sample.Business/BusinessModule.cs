using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Pixel.Sample.Business.Manager;

namespace Pixel.Sample.Business
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            // Register Managers
            builder.RegisterAssemblyTypes(typeof(FooManager).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.IsClosedTypeOf(typeof(IBaseManager<,>))))
                .PropertiesAutowired()
                .AsImplementedInterfaces();
        }
    }
}
