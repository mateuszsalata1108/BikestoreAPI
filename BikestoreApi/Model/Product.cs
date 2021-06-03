using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikestoreApi.Model
{
    public class Product
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string manufacturer { get; set; }
        public int price { get; set; }
        public int availability { get; set; }
    }
}
