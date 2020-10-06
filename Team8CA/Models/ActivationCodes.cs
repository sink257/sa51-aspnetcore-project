using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class ActivationCodes
    {
        public int Id { set; get; }
        public int CartItemId { get; set; }
        public string ActivationCode { get; set; }

        public ActivationCodes(int cartItemId, string activationCode)
        {
            CartItemId = cartItemId;
            ActivationCode = activationCode;
        }
    }
}
