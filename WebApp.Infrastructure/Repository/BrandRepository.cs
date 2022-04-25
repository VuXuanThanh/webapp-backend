using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;

namespace WebApp.Infrastructure.Repository
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
