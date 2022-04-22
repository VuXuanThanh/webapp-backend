﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;

namespace WebApp.Core.Interface.Repository
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        Task<int> Insert(string orderId);
    }
}
