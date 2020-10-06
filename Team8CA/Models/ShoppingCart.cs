using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Team8CA.Models
{
    public class ShoppingCart
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerID { get; set; } //to get customerID

        [Required]
        public DateTime OrderCreationTime { get; set; }

        public DateTime OrderTime {get;set;}

        public bool IsCheckOut { get; set; }

        public virtual Products products { get; set; } //to get pricing and productID

        public int Quantity { get; set; }

        
        public int Total { get; set; }


        public double SubTotal { get; set; }

        public virtual ICollection<PurchasedItems> CartItems { get; set; }

        public ShoppingCart()
        {
            CartItems = new List<PurchasedItems>();
        }

        public ShoppingCart(int customerId)
        {
            CustomerID = customerId;
            OrderCreationTime = DateTime.Now;
            IsCheckOut = false;
            CartItems = new List<PurchasedItems>();
        }



    }
}
