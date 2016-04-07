using Pixel.Sample.Core.Domain.Base;

namespace Pixel.Sample.Core.Domain
{
    public class Bar : BaseEntityIdent<int>
    {
        public virtual string Title { get; set; }
    }
}