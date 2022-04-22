using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;

namespace WebApp.Core.Interface.Repository
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task<int> Delete(string userId, string productId);

        Task<int> GetAllRecord(string userId);

        Task<List<CartItem>> GetCartItemsByUserId(string userId, bool order);

        Task<int> Update(List<ProductItem> list);

      
    }

}
