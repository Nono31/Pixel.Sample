using System;
using System.Collections.Generic;
using Pixel.Sample.Core.Domain.Base;

namespace Pixel.Sample.Business.Manager
{
    public interface IBaseManager
    { }

    /// <summary>
    /// Manager of User
    /// </summary>
    public interface IBaseManager<TEntity, TKey> : IBaseManager where TEntity : BaseEntityIdent<TKey> where TKey : IEquatable<TKey>
    {
        void Delete(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(TKey id);

        void Save(TEntity entity);
    }

}