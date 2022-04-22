using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;

namespace WebApp.Core.Interface.Services
{
    public interface IOrderItemService : IBaseService<OrderItem>
    {
        Task<int> Insert(string userId);
    }
}
