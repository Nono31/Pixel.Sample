using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Caches.SysCache;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Pixel.Sample.Data.Mapping;

namespace Pixel.Sample.Data
{
    public class DataInitializer
    {
        private static ISessionFactory _sessionFactory;

        internal ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        internal void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(x => x.FromConnectionStringWithKey("DefaultConnection"))
                    .ShowSql()
                )
                .Cache(c => c.ProviderClass<SysCacheProvider>()
                    .UseMinimalPuts()
                    .UseQueryCache()
                    .UseSecondLevelCache())
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<FooMap>())
                .CurrentSessionContext<WebSessionContext>()
                .ExposeConfiguration(cfg =>
                {
                    new SchemaExport(cfg).Create(true, true);
                    cfg.Cache(x => x.DefaultExpiration = 500);
                })
                .BuildSessionFactory();
        }
    }
}