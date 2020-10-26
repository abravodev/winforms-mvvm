using Dapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Exceptions;

namespace UserManager.BusinessLogic.DataAccess
{
    public static class DapperExtensions
    {
        public static async Task<List<TSource>> ToListAsync<TSource>(this Task<IEnumerable<TSource>> source)
            => (await source).ToList();

        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> source)
            => (await source).FirstOrDefault();

        /// <summary>
        /// Execute query to return first element. It throws <see cref="NotFoundException{T}"/> if not found
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static async Task<TSource> GetFirstAsync<TSource>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var item = await cnn.QueryFirstOrDefaultAsync<TSource>(sql, param, transaction, commandTimeout, commandType);
            if(item == null)
            {
                throw new NotFoundException<TSource>($"{typeof(TSource).Name} not found {JsonConvert.SerializeObject(param)}");
            }
            return item;
        }
    }
}
