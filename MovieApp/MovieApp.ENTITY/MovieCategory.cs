using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.ENTITY
{
    public class MovieCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
