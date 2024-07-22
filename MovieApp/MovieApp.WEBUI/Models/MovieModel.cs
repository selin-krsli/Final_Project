using MovieApp.ENTITY;

namespace MovieApp.WEBUI.Models
{
    public class MovieModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Url { get; set; }
        public string MovieStory { get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public int CategoryId { get; set; }
        public List<MovieCategory> MovieCategories { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
