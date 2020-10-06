using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team8CA.Models
{
    [Table("Products")]
    public class GalleryProducts
    {
        //Primary keu
        [Column("ProductID")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

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

      
    }
}
