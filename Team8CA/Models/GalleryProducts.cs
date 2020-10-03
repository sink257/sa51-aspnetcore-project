using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class GalleryProducts
    {
        public string ProductName { get; set; }
        public string ProductID{ get; set; }
        public string ProductPic { get; set; }
        public double ProductPrice { get; set; }
        public bool ProductAvailability { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }

        public List<GalleryProducts> GetList
        {
            get
            {
                return new List<GalleryProducts>()
                {
                    new GalleryProducts() { ProductName = "Avira", ProductID = "01" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "02" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "03" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "04" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "05" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "06" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "07" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "08" },

                    new GalleryProducts() { ProductName = "Avira", ProductID = "09" },

                };
            }
        }
    }
}
