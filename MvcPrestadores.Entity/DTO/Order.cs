using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcPrestadores.Entity.DTO
{
    public class Order
    {
        public string idCustomer { get; set; }
        public List<OrderDetails> details { get; set; }
    }

}
