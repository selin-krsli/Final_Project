using MovieApp.ENTITY;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.WEBUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        //[Required(ErrorMessage = "CategoryName is a mandatory field to fill in.")]
        //[StringLength(50,MinimumLength = 5,ErrorMessage="Character length must be between 5 and 50!")]
        public string? Name { get; set; }

        //[Required(ErrorMessage = "Url is a mandatory field to fill in.")]
        public string? Url { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
