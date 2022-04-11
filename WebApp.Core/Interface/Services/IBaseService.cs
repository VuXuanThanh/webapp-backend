using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Interface.Services
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Get all records in database
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// get entity by entityId
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task<T> GetById(string entityId);

        /// <summary>
        /// Insert a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Insert(T entity);

        /// <summary>
        /// Update by Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Update(Guid entityId, T entity);

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task<int> Delete(Guid entityId);

        /// <summary>
        /// filter by options
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<Object> Filter(string searchString, int pageSize, int pageIndex);
    }
}
