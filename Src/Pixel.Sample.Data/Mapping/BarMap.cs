using System.Security.AccessControl;
using Pixel.Sample.Core.Domain;

namespace Pixel.Sample.Data.Mapping
{
    public class BarMap : BaseMap<Bar>
    {
        public BarMap() : base("Bar")
        {
            Map(x => x.Title)
                .Column("Title")
                .Length(255)
                .Not.Nullable();
        }
    }
}