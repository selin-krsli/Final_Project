using MovieApp.ENTITY;

namespace MovieApp.WEBUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
