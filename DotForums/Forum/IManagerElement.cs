using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotForums.Forum
{
    interface IManagerElement<T>
    {
        /// <summary>
        /// Create ForumModel Object Asynchronously
        /// </summary>
        /// <typeparam name="T">Type of ForumModel</typeparam>
        /// <param name="parameters">Required parameters to create the object</param>
        /// <returns></returns>
        Task<T> CreateAsync(IDictionary<string, string> Body);
        /// <summary>
        /// Get ForumModel Object Asynchronously
        /// </summary>
        /// <typeparam name="T">Type of Forum Model</typeparam>
        /// <param name="ID">Database ID of the ForumModel object</param>
        /// <returns></returns>
        Task<T> GetAsync(ulong ID);
        Task<List<T>> GetAsync();
        Task<List<T>> GetAsync(Expression<Func<T, bool>> lamda, int index, int size);
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
        Task<T> UpdateAsync(ulong ID, IDictionary<string, object> Body);
    }
}
