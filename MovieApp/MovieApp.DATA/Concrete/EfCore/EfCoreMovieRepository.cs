using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieContext>, IMovieRepository
    {
        public List<Movie> GetPopularMovies()
        {
            using(var context = new MovieContext())
            {
                return context.Movies.ToList();
            }
        }

        public List<Movie> GetTop5Movies()
        {
            throw new NotImplementedException();
        }
    }
}
