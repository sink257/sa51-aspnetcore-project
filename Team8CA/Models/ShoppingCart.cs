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
        public int Id { get; set; } //this is the cartID

        public int CustomerId { get; set; } //to get customerID

        [Required]
        public DateTime OrderCreationTime { get; set; }

        public DateTime OrderTime {get;set;}


        public bool IsCheckOut { get; set; } //if checkout or not

        //public virtual ICollection<ShoppingCartItems> ShoppingCartItems { get; set; }

        public ShoppingCart()
        {
            //ShoppingCartItems = new List<ShoppingCartItems>();
        }

        public ShoppingCart(int customerId)
        {
            CustomerId = customerId;
            OrderCreationTime = DateTime.Now;
            IsCheckOut = false;
            //ShoppingCartItems = new List<ShoppingCartItems>();
        }

        [NotMapped]
        public int NoOfProductTypes { get; set; }

        [NotMapped]
        public double Total { get; set; }

        [NotMapped]
        public double SubTotal { get; set; }


    }
}
