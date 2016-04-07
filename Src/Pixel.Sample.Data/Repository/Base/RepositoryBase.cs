using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Pixel.Sample.Core.Domain.Base;
using Pixel.Sample.Data.Utils;

namespace Pixel.Sample.Data.Repository.Base
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ISession _session;

        public RepositoryBase(ISession session)
        {
            _session = session;
        }
        protected ISession Session
        {
            get { return _session; }
        }

        public TEntity Get<TKey>(TKey id)
        {
            return WithinTransaction(() => Session.Get<TEntity>(id));
        }

        public void Save(TEntity entity)
        {
            WithinTransaction(() => Session.SaveOrUpdate(entity));
        }

        public void Save(IEnumerable<TEntity> entities)
        {
            using (var localTransaction = Session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                foreach (var entity in entities)
                {
                    WithinTransaction(() => Session.SaveOrUpdate(entity));
                }
                localTransaction.Commit();
            }
        }

        public void Delete(TEntity entity)
        {
            WithinTransaction(() => Session.Delete(entity));
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            using (var localTransaction = Session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                foreach (var entity in entities)
                {
                    WithinTransaction(() => Session.Delete(entity));
                }
                localTransaction.Commit();
            }
        }
        
        public IEnumerable<TEntity> Query(ISpecification<TEntity> specification)
        {
            return WithinTransaction(() => Query().Where(specification.Predicate).ToArray());
        }

        public IEnumerable<TEntity> GetAll()
        {
            return WithinTransaction(() => Query().ToArray());
        }

        protected IQueryable<TEntity> Query()
        {
            return CreateQuery();
        }

        protected IQueryable<TEntity> CreateQuery()
        {
            return Session.Query<TEntity>();
        }


        protected T WithinTransaction<T>(Func<T> functionToExecute, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Session.Transaction != null && Session.Transaction.IsActive)
                return functionToExecute();

            using (var localTransaction = Session.BeginTransaction(isolationLevel))
            {
                var entity = functionToExecute();
                localTransaction.Commit();
                return entity;
            }
        }

        protected void WithinTransaction(Action actionToExecute, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Session.Transaction != null && Session.Transaction.IsActive)
                actionToExecute();

            using (var localTransaction = Session.BeginTransaction(isolationLevel))
            {
                actionToExecute();
                localTransaction.Commit();
            }
        }

    }
}