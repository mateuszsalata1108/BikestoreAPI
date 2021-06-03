using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikestoreApi.Model
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        {

        }
    }
}
