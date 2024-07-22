using Microsoft.EntityFrameworkCore;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieContext>, IMovieRepository
    {
        public List<Movie> GetHomePageMovies()
        {
            using(var context = new MovieContext())
            {
                return context.Movies.Where(s=>s.IsApproved && s.IsHome).ToList();
            }
        }

        public int GetMovieByCategory(string category)
        {
            using (var context = new MovieContext())
            {
                var movies = context.Movies.Where(s=>s.IsApproved).AsQueryable();
                if(!string.IsNullOrEmpty(category))
                {
                    movies = movies
                                .Include(s => s.MovieCategories)
                                .ThenInclude(s => s.Category)
                                .Where(s => s.MovieCategories.Any(l => l.Category.Url == category));
                }
                return movies.Count();
            }
        }

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

        public List<Movie> GetMovieWithCategories(string category,int page,int pageSize)
        {
            using(var context = new MovieContext())
            {
                //db'ye gitmeden/sorguyu çalıştırmadan kriter ekleyebiliyorum.
                var movies = context.Movies.Where(s=>s.IsApproved).AsQueryable(); 
                if (!string.IsNullOrEmpty(category))
                {
                    movies = movies.Include(s => s.MovieCategories)
                                   .ThenInclude(s => s.Category)
                                   .Where(s => s.MovieCategories.Any(m => m.Category.Url == category));
                }
                //page varsayılan değeri = 1 olursa direkt Take() metodu çalışacak.
                return movies.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }

        public List<Movie> GetSearchResult(string searchingWord)
        {
            using( var context = new MovieContext())
            {
                var movies = context.Movies
                                    .Where(s => s.IsApproved && (s.MovieName.ToLower().Contains(searchingWord.ToLower()) || s.MovieStory.ToLower().Contains(searchingWord.ToLower())))
                                    .AsQueryable();
                return movies.ToList();
                                    
            }
        }
    }
}
