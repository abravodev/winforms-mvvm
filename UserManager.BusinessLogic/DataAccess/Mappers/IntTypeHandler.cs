using Dapper;
using System;
using System.Data;

namespace UserManager.BusinessLogic.DataAccess.Mappers
{
    /// <summary>
    /// Inspired by <see cref="SqlMapper.StringTypeHandler{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IntTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override T Parse(object value)
        {
            if (value == null || value is DBNull) return default(T);
            return Parse(Convert.ToInt32(value));
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = value == null ? (object) DBNull.Value : Format(value);
        }

        protected abstract int Format(T xml);

        protected abstract T Parse(int xml);
    }
}
