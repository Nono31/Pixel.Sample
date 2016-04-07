using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixel.Sample.Core.Domain.Base;

namespace Pixel.Sample.Core.Domain
{
    public class Foo : BaseEntityIdent<int>
    {
        public virtual string Title { get; set; }
        public virtual Bar Bar { get; set; }
    }
}
