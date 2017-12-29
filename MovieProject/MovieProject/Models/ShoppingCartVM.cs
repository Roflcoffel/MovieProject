using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieProject.Models {
    public class ShoppingCartVM {
         
        public Dictionary<Movie,int> Cart { get; set; }
    }
}