using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Interface.Repository
{
    public interface IBaseRepository<T>
    {
        public int Insert();
    }
}
