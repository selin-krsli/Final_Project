using MovieApp.ENTITY;

namespace MovieApp.DATA.Abstract
{
    public interface IMovieRepository:IRepository<Movie>
    {
        List<Movie> GetPopularMovies();
        List<Movie> GetTop5Movies();
        Movie GetMovieWithCategories(int id);//movie info & which category
    }
}
