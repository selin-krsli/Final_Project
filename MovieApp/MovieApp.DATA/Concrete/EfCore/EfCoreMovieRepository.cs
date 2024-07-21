using Microsoft.EntityFrameworkCore;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieContext>, IMovieRepository
    {
        public Movie GetMovieDetails(string url)
        {
            using(var context = new MovieContext())
            {
                return context.Movies
                              .Where(m => m.Url == url)
                              .Include(m => m.MovieCategories)
                              .ThenInclude(m => m.Category)
                              .FirstOrDefault();//LEFT JOİN; Movie -> MovieCategory -> Category
            }
        }

        public List<Movie> GetMovieWithCategories(string category)
        {
            using(var context = new MovieContext())
            {
                var movies = context.Movies.AsQueryable(); //db'ye gitmeden/sorguyu çalıştırmadan kriter ekleyebiliyorum.
                if (!string.IsNullOrEmpty(category))
                {
                    movies = movies.Include(s => s.MovieCategories)
                                   .ThenInclude(s => s.Category)
                                   .Where(s => s.MovieCategories.Any(m => m.Category.Url == category));
                }

                return movies.ToList();
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
