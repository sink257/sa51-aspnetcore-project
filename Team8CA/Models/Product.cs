using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team8CA.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team8CA.Models
{
     [Table("Products")]
    public class Product
    {
        //Primary key
        [Column("ProductID")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("ProductName")]
        [Required]
        public string ProductName { get; set; }

        [Column("ProductPic")]
        [Required]
        public string ProductPic { get; set; }

        [Column("ProductPrice")]
        [Required]
        public double ProductPrice { get; set; }

        [Column("ProductAvailability")]
        [Required]
        public bool ProductAvailability { get; set; }

        [Column("ProductDescription")]
        [Required]
        public string ProductDescription { get; set; }

        [Column("Category")]
        [Required]
        public string ProductCategory { get; set; }

        public virtual Category Category { get; set; }
      
        
        public Product(int ID, string ProductName, double ProductPrice, bool ProductAvailability, string ProductDescription, string ProductCategory)
        { 
            this.ID = ID;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.ProductAvailability = ProductAvailability;
            this.ProductDescription = ProductDescription;
            this.ProductCategory = ProductCategory;
        }
    }

}
