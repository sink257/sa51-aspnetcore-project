﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team8CA.DataAccess
{
    [Table("Customers")]
    public class Customer
    {
        [Column("CustomerID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CustomerID { get; set; }

        [Column("Username")]
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Column("Password")]
        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        [ForeignKey("CustomerID")]
        public virtual List<Order> Orders { get; set; }
    }
}