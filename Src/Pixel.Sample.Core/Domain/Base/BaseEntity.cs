using System;

namespace Pixel.Sample.Core.Domain.Base
{
    public abstract class BaseEntity
    {
    }


    public abstract class BaseEntityIdent<TIdent> : BaseEntity where TIdent : IEquatable<TIdent>
    {
        public virtual TIdent Id { get; protected set; }

       public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return Equals(other as BaseEntityIdent<TIdent>);
        }

        public virtual bool Equals(BaseEntityIdent<TIdent> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (Id.Equals(default(TIdent)))
            {
                return false;
            }
            return Id.Equals(other.Id);
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int multiplier = 31;
                int hash = GetType().GetHashCode();
                hash = hash*multiplier + Id.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(BaseEntityIdent<TIdent> x, BaseEntityIdent<TIdent> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntityIdent<TIdent> x, BaseEntityIdent<TIdent> y)
        {
            return !(x == y);
        }
    }
}