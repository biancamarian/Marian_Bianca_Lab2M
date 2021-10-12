using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marian_Bianca_Lab2.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int BookID { get; set; }
        public DateTime OrderDate { get; set; }//lab3
        public Customer Customer { get; set; }
        public Book Book { get; set; }
    }
}
