using System.Data;
using System.Linq;
using Autofac;
using NHibernate;
using NHibernate.Context;
using Pixel.Sample.Data.Repository;
using Pixel.Sample.Data.Repository.Base;

namespace Pixel.Sample.Data
{

    public class DataModule : Module
    {
        public const string Name = "PixelSample";

        protected string GetUniqueName()
        {
            return Name;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //Register Data initializer
            builder.RegisterInstance(new DataInitializer())
                .Named<DataInitializer>(GetUniqueName())
                .As<DataInitializer>()
                .SingleInstance();

            //Register Session Factory
            builder.Register(ResolveSessionFactory)
                .Named<ISessionFactory>(GetUniqueName())
                .As<ISessionFactory>()
                .SingleInstance();

            //Register Session
            builder.Register(ResolveSession)
                .Named<ISession>(GetUniqueName())
                .As<ISession>()
                .InstancePerRequest();

            //Register Repository
            builder.RegisterAssemblyTypes(typeof(FooRepository).Assembly)
                .Where(
                    t =>
                        t.GetInterfaces()
                            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

        }


        private ISessionFactory ResolveSessionFactory(IComponentContext componentContext)
        {
            var dataInitializer = componentContext.ResolveNamed<DataInitializer>(GetUniqueName());
            return dataInitializer.SessionFactory;
        }

        private ISession ResolveSession(IComponentContext componentContext)
        {
            var sessionFactory = componentContext.ResolveNamed<ISessionFactory>(GetUniqueName());
            if (CurrentSessionContext.HasBind(sessionFactory))
            {
                return sessionFactory.GetCurrentSession();
            }
            var session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }
    }
}
