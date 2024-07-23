using MovieApp.BUSINESS.Abstract;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;

namespace MovieApp.BUSINESS.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public void Create(Movie entity)
        {
            _movieRepository.Create(entity);
        }

        public void Delete(Movie entity)
        {
            _movieRepository.Delete(entity);
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public Movie GetById(int id)
        {
            return _movieRepository.GetById(id);
        }

        public Movie GetByIdWithCategories(int id)
        {
            return _movieRepository.GetByIdWithCategories(id);  
        }

        public List<Movie> GetHomePageMovies()
        {
            return _movieRepository.GetHomePageMovies();
        }

        public int GetMovieByCategory(string category)
        {
            return _movieRepository.GetMovieByCategory(category);
        }

        public Movie GetMovieDetails(string url)
        {
           return _movieRepository.GetMovieDetails(url);
        }

        public List<Movie> GetMovieWithCategories(string categoryName,int page,int pageSize)
        {
            return _movieRepository.GetMovieWithCategories(categoryName,page,pageSize);
        }

        public List<Movie> GetSearchResult(string searchingWord)
        {
            return _movieRepository.GetSearchResult(searchingWord);
        }

        public void Update(Movie entity)
        {
            _movieRepository.Update(entity);
        }

        public void Update(Movie entity, int[] categoryIds)
        {
            _movieRepository.Update(entity, categoryIds);
        }
    }
}
