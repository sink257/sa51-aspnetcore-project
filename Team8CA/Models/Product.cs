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
    public class Products
    {
        //Primary key
        [Key]
        [Column("ProductID")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        
        public string ProductPic { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public bool ProductAvailability { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public string ProductCategory { get; set; }

        public string LongProductDesc { get; set; }

        public virtual Category Category { get; set; }

        public Products() { }

        public Products(string ProductName, string ProductPic, double ProductPrice, bool ProductAvailability, string ProductDescription, string ProductCategory, string LongProductDesc)
        {
            this.ProductName = ProductName;
            this.ProductPic = ProductPic;
            this.ProductPrice = ProductPrice;
            this.ProductAvailability = ProductAvailability;
            this.ProductDescription = ProductDescription;
            this.ProductCategory = ProductCategory;
            this.LongProductDesc = LongProductDesc;
        }

        public virtual IList<Review> Reviews { get; set; }
    }

}
