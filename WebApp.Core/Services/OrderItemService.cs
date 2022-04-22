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
    public class OrderItemService : BaseService<OrderItem>, IOrderItemService
    {
        IOrderItemRepository _orderItemRepository;
        public OrderItemService(IOrderItemRepository orderItemRepository) : base(orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<int> Insert(string userId)
        {
            var res = await _orderItemRepository.Insert(userId);
            return res;
        }
    }
}
