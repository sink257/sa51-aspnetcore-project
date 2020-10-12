using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Team8CA.DataAccess;

namespace Team8CA.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

        //[Required]
        //public string PhoneNumber { get; set; }
        //[Required]
        //public string StreetAddress { get; set; }
        //[Required]
        //public string City { get; set; }
        //[Required]
        //public string State { get; set; }
        //[Required]
        //public string PostalCode { get; set; }
        //[Required]
        //public string Name { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
        public Order(string customerId)
        {
            CustomerId = customerId;
            OrderDate = DateTime.Now;
            OrderDetails = new List<OrderDetails>();

        }
    }
}


