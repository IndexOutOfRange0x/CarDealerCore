using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerCore.Models
{
    public class Car
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(17, ErrorMessage = "VIN номер должен состоять из 17 символов")]
        public string VIN { get; set; }

        [Required]
        public string Mark { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
