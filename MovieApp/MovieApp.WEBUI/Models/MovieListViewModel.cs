using MovieApp.ENTITY;

namespace MovieApp.WEBUI.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPageIndex { get; set; }
        public string? CurrentCategory { get; set; }
        public int TotalPages() { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
    }
    public class MovieListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
