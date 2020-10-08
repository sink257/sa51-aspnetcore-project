using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team8CA.Models
{
    public class ProductParameters
    {
        const int maxPageSize = 6;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 1;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize: value;
            }
        }

    }
}
