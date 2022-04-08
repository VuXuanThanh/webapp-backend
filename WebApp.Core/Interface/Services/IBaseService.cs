using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Interface.Services
{
    public interface IBaseService<T>
    {
        public int Insert();
    }
}
