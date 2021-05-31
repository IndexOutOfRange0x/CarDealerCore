using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarDealerCore.Models
{
    public class User : IdentityUser
    {
        public virtual List<Sale> Sale { get; set; } = new List<Sale>();
    }
}
