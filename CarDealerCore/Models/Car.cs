using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerCore.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
