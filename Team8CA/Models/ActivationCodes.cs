using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class ActivationCodes
    {
        public int Id { set; get; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ActivationCode { get; set; }

        public ActivationCodes()
        {

        }

        public ActivationCodes(int orderid, string activationCode, int productsid)
        {
            ProductId = productsid;
            OrderId = orderid;
            ActivationCode = activationCode;
        }
        
    }
}
