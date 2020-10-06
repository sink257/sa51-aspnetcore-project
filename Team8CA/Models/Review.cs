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
        // Primary Key
        [Column("ReviewID")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Column("ReviewDate")]
        [Required]
        public DateTime ReviewDate { get; set; }

        [Required]
        public int StarRating { get; set; }

        public string ReviewDetails { get; set; }


    }
}

