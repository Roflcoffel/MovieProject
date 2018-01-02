using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieProject.Models {
    public class MovieComparer : IEqualityComparer<Movie> {
        public bool Equals(Movie x, Movie y)
        { 
            if (ReferenceEquals(x, y)) return true;

            
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            
            return x.Id == y.Id && x.Title == y.Title;
        }

        public int GetHashCode(Movie obj)
        {
            return 1;
        }
    }
}