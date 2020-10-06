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
        //Primary keu
        [Column("ProductID")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductPic { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public bool ProductAvailability { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public string ProductCategory { get; set; }

        public virtual Category Category { get; set; }
      
    }

}
