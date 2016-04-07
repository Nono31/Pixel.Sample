using Pixel.Sample.Core.Domain;
using Pixel.Sample.Data.Repository.Base;

namespace Pixel.Sample.Business.Manager
{
    public class FooManager : BaseManager<Foo, int>, IFooManager
    {
        public FooManager(IRepository<Foo> repository) : base(repository)
        {
        }
    }
}