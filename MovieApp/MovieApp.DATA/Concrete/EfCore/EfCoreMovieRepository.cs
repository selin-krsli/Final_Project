using Microsoft.EntityFrameworkCore;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieContext>, IMovieRepository
    {
        public Movie GetMovieWithCategories(int id)
        {
            using(var context = new MovieContext())
            {
                return context.Movies
                              .Where(m => m.MovieId == id)
                              .Include(m => m.MovieCategories)
                              .ThenInclude(m => m.Category)
                              .FirstOrDefault();//LEFT JOİN; Movie -> MovieCategory -> Category
            }
        }

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
