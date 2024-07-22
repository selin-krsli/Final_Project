using MovieApp.ENTITY;

namespace MovieApp.WEBUI.Models
{
    public class MovieCategoryViewModel
    {
        public Movie Movie { get; set; }
        public List<Category> Categories { get; set; }
    }
}
