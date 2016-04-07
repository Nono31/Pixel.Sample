using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pixel.Sample.Core.Domain.Base;
using Pixel.Sample.Data.Utils;

namespace Pixel.Sample.Data.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Get<TKey>(TKey id);
        void Save(TEntity entity);
        void Save(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Query(ISpecification<TEntity> specification);
        IEnumerable<TEntity> GetAll();
    }
}
