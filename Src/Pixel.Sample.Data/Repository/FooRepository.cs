using NHibernate;
using Pixel.Sample.Core.Domain;
using Pixel.Sample.Data.Repository.Base;

namespace Pixel.Sample.Data.Repository
{
    public class FooRepository : RepositoryBase<Foo>
    {
        public FooRepository(ISession session) : base(session)
        {
        }
    }
}
