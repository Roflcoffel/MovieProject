using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieProject.Models {
    public class CheckOutVM {

        public Customer Customer { get; set; }
        public List<Movie> ShoppingCart { get; set; }
        public int TotalCost { get; set; }

    }
}