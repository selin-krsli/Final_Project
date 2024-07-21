using MovieApp.ENTITY;

namespace MovieApp.DATA.Abstract
{
    public interface IMovieRepository:IRepository<Movie>
    {
        List<Movie> GetPopularMovies();
        List<Movie> GetTop5Movies();
        List<Movie> GetMovieWithCategories(string categoryName);//movie info & which category
        Movie GetMovieDetails(string url);
    }
}
