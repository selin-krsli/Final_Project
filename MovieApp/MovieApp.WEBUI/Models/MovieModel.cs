using MovieApp.ENTITY;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.WEBUI.Models
{
    public class MovieModel
    {
        public int MovieId { get; set; }

        //[Required(ErrorMessage = "MovieName is a mandatory field to fill in.")]
        //[StringLength(50, MinimumLength =5, ErrorMessage = "Character length must be between 5 and 50!")]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "Url is a mandatory field to fill in.")]
        public string Url { get; set; }

        //[Required(ErrorMessage = "MovieStory is a mandatory field to fill in.")]
        //[StringLength(2000, MinimumLength = 20, ErrorMessage = "Character length must be between 20 and 2000!")]
        public string MovieStory { get; set; }

        //[Required(ErrorMessage = "ReleaseYear is a mandatory field to fill in.")]
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }

        //[Required(ErrorMessage = "Director is a mandatory field to fill in.")]
        public string Director { get; set; }

        //[Required(ErrorMessage = "Price is a mandatory field to fill in.")]
        //[Range(50,1000, ErrorMessage = "A value between 50 and 10000 must be entered for the price!")]
        public decimal? Price { get; set; }

        //[Required(ErrorMessage = "Image is a mandatory field to fill in.")]
        public string Image { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public int CategoryId { get; set; }
        public List<MovieCategory> MovieCategories { get; set; }
        public List<Category> SelectedCategories { get; set; } = new List<Category>();
    }
}
