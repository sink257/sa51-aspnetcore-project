using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team8CA.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team8CA.Models
{
    [Table("CustomerLogin")]
    public class Customer
    {
        [Column("CustomerId")]
        [Key]
        [Required]
        public string CustomerID { get; set; }

        [Column("Username")]
        [Required]
        [StringLength(32)]
        public string Username { get; set; }

        [Column("Password")]
        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Column("FirstName")]
        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }

        public Customer()
        { 

        }

        public Customer(string CustomerID, string Username, string Password, string FirstName)
        {
            this.CustomerID = CustomerID;
            this.Username = Username;
            this.Password = Password;
            this.FirstName = FirstName;
            //remember to hash the password in LoginUtility
            //this.Password = LoginUtility.Password;
        }        
    }
}
