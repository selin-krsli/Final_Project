using Microsoft.EntityFrameworkCore;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreMovieRepository : EfCoreGenericRepository<Movie, MovieContext>, IMovieRepository
    {
        public Movie GetByIdWithCategories(int id)
        {
            using(var context = new MovieContext())
            {
                return context.Movies
                               .Where(s => s.MovieId == id)
                               .Include(s => s.MovieCategories)
                               .ThenInclude(s => s.Category)
                               .FirstOrDefault();
            }
        }

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

        public void Update(Movie entity, int[] categoryIds)
        {
            using(var context = new MovieContext())
            {
                var movie = context.Movies
                    .Include(s => s.MovieCategories)
                    .FirstOrDefault(s=>s.MovieId == entity.MovieId);

                if (movie != null)
                {
                    movie.MovieName = entity.MovieName;
                    movie.MovieStory = entity.MovieStory;
                    movie.Url = entity.Url;
                    movie.Price = entity.Price;
                    movie.Director = entity.Director;
                    movie.Image = entity.Image;
                    movie.IsHome = entity.IsHome;
                    movie.IsApproved = entity.IsApproved;

                    movie.MovieCategories = categoryIds.Select(c=> new MovieCategory()
                    {
                        MovieId = entity.MovieId,
                        CategoryId = c
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}
