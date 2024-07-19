using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieContext>, IMovieRepository
    {
        public List<Movie> GetPopularMovies()
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetTop5Movies()
        {
            throw new NotImplementedException();
        }
    }
}
