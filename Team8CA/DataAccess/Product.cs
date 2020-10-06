//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Data.Entity;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Team8CA.DataAccess
//{
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
