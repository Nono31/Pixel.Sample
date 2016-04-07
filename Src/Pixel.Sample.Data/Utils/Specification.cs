using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pixel.Sample.Data.Utils
{
    /// <summary>
    ///     Specification pattern
    ///     Specification to specify linq expression of <see cref="T" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
        bool IsSatisfiedBy(T entity);
    }


    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledPredicate;
        private Expression<Func<T, bool>> _predicate;

        protected abstract Expression<Func<T, bool>> GetPredicate();

        protected Expression<Func<T, bool>> And(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
            {
                return right;
            }
            if (right == null)
            {
                return left;
            }
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }

        protected Expression<Func<T, bool>> Or(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
            {
                return right;
            }
            if (right == null)
            {
                return left;
            }
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, invokedExpr), left.Parameters);
        }


        public static DirectSpecification<T> operator &(SpecificationBase<T> leftSpec, SpecificationBase<T> rightSpec)
        {
            var rightInvoke = Expression.Invoke(rightSpec.Predicate, leftSpec.Predicate.Parameters.Cast<Expression>());
            var newExpression = Expression.MakeBinary(ExpressionType.AndAlso, leftSpec.Predicate.Body, rightInvoke);
            return
                new DirectSpecification<T>(Expression.Lambda<Func<T, bool>>(newExpression, leftSpec.Predicate.Parameters));
        }


        public static DirectSpecification<T> operator |(SpecificationBase<T> leftSpec, SpecificationBase<T> rightSpec)
        {
            var rightInvoke = Expression.Invoke(rightSpec.Predicate, leftSpec.Predicate.Parameters.Cast<Expression>());
            var newExpression = Expression.MakeBinary(ExpressionType.OrElse, leftSpec.Predicate.Body, rightInvoke);
            return
                new DirectSpecification<T>(Expression.Lambda<Func<T, bool>>(newExpression, leftSpec.Predicate.Parameters));
        }

        public static DirectSpecification<T> operator !(SpecificationBase<T> spec)
        {
            var newExpression = Expression.Not(spec.Predicate);
            return new DirectSpecification<T>(Expression.Lambda<Func<T, bool>>(newExpression));
        }

        #region ISpecification<T> Members

        public Expression<Func<T, bool>> Predicate
        {
            get
            {
                if (_predicate == null)
                {
                    _predicate = GetPredicate();
                }
                return _predicate;
            }
        }

        public bool IsSatisfiedBy(T entity)
        {
            if (_compiledPredicate == null)
            {
                _compiledPredicate = Predicate.Compile();
            }
            return _compiledPredicate.Invoke(entity);
        }

        #endregion
    }


    public class DirectSpecification<T> : SpecificationBase<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        public DirectSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate;
        }

        protected override Expression<Func<T, bool>> GetPredicate()
        {
            return _predicate;
        }
    }
}