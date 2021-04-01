using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Data.Models
{
    public class Cost
    {
        [Key]
        public int CostId { get; set; }
        public int Price { get; set; }
        public bool DiscountCode { get; set; }
    }
}
