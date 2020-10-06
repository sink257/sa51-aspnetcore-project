//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Data.Entity;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Team8CA.DataAccess
//{
//    [Table("Customers")]
//    public class Customer
//    {
//        [Column("CustomerID")]
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [Required]
//        public int CustomerID { get; set; }

//        [Column("Username")]
//        [Required]
//        [StringLength(20)]
//        public string Username { get; set; }

//        [Column("Password")]
//        [Required]
//        [StringLength(12)]
//        public string Password { get; set; }

//        [ForeignKey("CustomerID")]
//        public virtual List<Order> Orders { get; set; }
//    }
//    public class Order
//    {
//        [Column("OrderID")]
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [Required]
//        public int OrderID { get; set; }

//        [Column("OrderDate")]
//        [Required]
//        public DateTime OrderDate { get; set; }

//        [Required]
//        public int CustomerID { get; set; }
//    }
//    [Table("Products")]
//    public class Product
//    {
//        [Column("ProductID")]
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [Required]
//        public int ProductID { get; set; }

//        [Column("ProductName")]
//        [Required]
//        [StringLength(100)]
//        public string ProductName { get; set; }
//    }
//}
