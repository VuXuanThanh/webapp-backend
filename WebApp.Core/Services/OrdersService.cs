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
    public class OrdersService : BaseService<Orders>, IOrdersService
    {
        public OrdersService(IBaseRepository<Orders> baseRepository) : base(baseRepository)
        {
        }
    }
}
