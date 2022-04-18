using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interface.Repository;
using WebApp.Core.Interface.Services;

namespace WebApp.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> Delete(Guid entityId)
        {
            var res = await _baseRepository.Delete(entityId);
            return res;

        }

        public Task<object> Filter(string searchString, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            var result = await _baseRepository.GetAll();
            return result;
        }

        public async Task<T> GetById(string entityId)
        {
            var result = await _baseRepository.GetById(entityId);
            return result;

        }

        public int Insert()
        {
            var x = 10000;
            return x;
        }

        public async Task<int> Insert(T entity)
        {
            var res =await _baseRepository.Insert(entity);
            return res;
        }

        public Task<int> Update(Guid entityId, T entity)
        {
            throw new NotImplementedException();
        }

    }
}
