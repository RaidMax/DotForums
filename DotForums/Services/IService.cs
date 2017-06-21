using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using DotForums.Forum;

namespace DotForums.Services
{
    public interface IService<T>
    {
        /// <summary>
        /// Create ForumModel Object Asynchronously
        /// </summary>
        /// <typeparam name="T">Type of ForumModel</typeparam>
        /// <param name="parameters">Required parameters to create the object</param>
        /// <returns></returns>
        Task<T> CreateAsync(T Value);
        //Expression<Func<T, bool>> lamda = null, string include = null, int index = 0, int size = 0
        Task<ICollection<T>> GetAsync(ulong ID = 0, QueryOptions Options = null);
        /// <summary>
        /// Delete ForumModel Object Asynchronously
        /// </summary>
        /// <param name="ID">Database ID of the ForumModel object</param>
        /// <returns></returns>
        Task<T> DeleteAsync(ulong ID);
        /// <summary>
        /// Update ForumModel Object Asynchronously
        /// </summary>
        /// <typeparam name="T">Type of ForumModel</typeparam>
        /// <param name="toUpdate">Existing object</param>
        /// <returns></returns>
        Task<T> UpdateAsync(ulong ID, T Value);
    }
}
