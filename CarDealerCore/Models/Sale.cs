using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerCore.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime Date_Sold { get; set; }
        public string Status { get; set; }
    }
}
