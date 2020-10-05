using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team8CA.DataAccess
{
    public class Order
    {
        [Column("OrderID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderID { get; set; }

        [Column("OrderDate")]
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerID { get; set; }
    }
}
