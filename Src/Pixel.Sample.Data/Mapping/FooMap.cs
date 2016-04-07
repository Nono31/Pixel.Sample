using Pixel.Sample.Core.Domain;

namespace Pixel.Sample.Data.Mapping
{
    public class FooMap : BaseMap<Foo>
    {
        public FooMap() : base("Foo")
        {
            Map(x => x.Title)
                .Column("Title")
                .Length(255)
                .Not.Nullable();

            References(x => x.Bar)
                .Cascade.SaveUpdate()
                .Column("BarId")
                .Not.Nullable();
        }
    }
}