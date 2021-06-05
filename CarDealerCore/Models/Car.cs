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
        [Required(ErrorMessage = "Поле \"VIN\" является обязательным")]
        [StringLength(17, ErrorMessage = "VIN номер должен состоять из 17 символов", MinimumLength = 17)]
        public string VIN { get; set; }

        [Required(ErrorMessage = "Поле \"Марка\" является обязательным")]
        public string Mark { get; set; }

        [Required(ErrorMessage = "Поле \"Модель\" является обязательным")]
        public string Model { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Поле \"Цена\" является обязательным")]
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
