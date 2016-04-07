using System;
using System.Collections.Generic;
using Pixel.Sample.Core.Domain.Base;
using Pixel.Sample.Data.Repository.Base;

namespace Pixel.Sample.Business.Manager
{
    public abstract class BaseManager
    {
        //private static readonly ILogger InternalLogger = LogManager.GetCurrentClassLogger();

        //protected ILogger Logger
        //{
        //    get { return InternalLogger; }
        //}
    }
    public abstract class BaseManager<TEntity, TKey> : BaseManager, IBaseManager<TEntity, TKey> where TEntity : BaseEntityIdent<TKey> where TKey : IEquatable<TKey>
    {
        private readonly IRepository<TEntity> _repository;

        protected BaseManager(IRepository<TEntity> repository)
        {
            if(repository == null)
                throw new ArgumentNullException();
            _repository = repository;
        }

        protected IRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual TEntity GetById(TKey id)
        {
            return Repository.Get(id);
        }

        public virtual void Save(TEntity entity)
        {
            Repository.Save(entity);
        }
    }

}