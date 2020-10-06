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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CustomerId { get; set; }

        [Column("Username")]
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Column("Password")]
        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        public Customer() { }

        public Customer(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
            //remember to hash the password in LoginUtility
            //this.Password = LoginUtility.Password;
        }

    }
}
