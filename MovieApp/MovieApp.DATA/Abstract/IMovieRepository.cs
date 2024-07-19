using MovieApp.ENTITY;

namespace MovieApp.DATA.Abstract
{
    public interface IMovieRepository:IRepository<Movie>
    {
        List<Movie> GetPopularMovies();
        List<Movie> GetTop5Movies();
    }
}
