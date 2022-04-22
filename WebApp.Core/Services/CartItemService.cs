using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;
using WebApp.Core.Interface.Services;

namespace WebApp.Core.Services
{
    public class CartItemService : BaseService<CartItem>, ICartItemService
    {
        ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository):base(cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<int> Delete(string userId, string productId)
        {
            var res = await _cartItemRepository.Delete(userId, productId);
            return res;

        }


        public async Task<int> DeleteMultiple(List<ProductItem> list)
        {
            var count = 0;
            foreach (var item in list)
            {
                var res = await _cartItemRepository.Delete(item.UserId, item.ProductId);
                count += res;
            }
            return count;
        }

        public async Task<int> GetAllRecord(string userId)
        {
            var res = await _cartItemRepository.GetAllRecord(userId);
            return res;
        }

        public async Task<List<CartItem>> GetCartItemsByUserId(string userId, bool order)
        {
            var res = await _cartItemRepository.GetCartItemsByUserId(userId, order);
            return res;
        }

        public async Task<int> Update(List<ProductItem> list)
        {
            var res = await _cartItemRepository.Update(list);
            return res;
        }
    }
}
