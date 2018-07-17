using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FogDemo.WebApi.Db.Seed
{
    public class SeedData
    {
        private readonly MyDbContext _dbContext;

        public SeedData(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Build()
        {
            CreateOrgan();
        }

        private void CreateOrgan()
        {
            
        }
    }
}
