using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team8CA.Models
{
    [Table("Reviews")]
    public class Review
    {

        public Review(int productID, string customerName, int starRating, string reviewDetails, DateTime reviewDate)
        {
            this.ProductID = productID;
            this.CustomerName = customerName;
            this.StarRating = starRating;
            this.ReviewDetails = reviewDetails;
            this.ReviewDate = reviewDate;
        }
        // Primary Key
        [Column("ReviewID")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]//Foreign key
        public int ProductID { get; set; }


        public string CustomerName { get; set; }
        public int CustomerId { get; set; }

        [Column("ReviewDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)] 
        public DateTime ReviewDate { get; set; }

        [Required]
        public int StarRating { get; set; }
        
        [Required]
        public string ReviewDetails { get; set; }

        //public virtual Customer Customer { get; set; }
        public virtual Products Products { get; set; }
    }
}

