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

        public Movie GetMovieWithCategories(int id)
        {
           return _movieRepository.GetMovieWithCategories(id);
        }

        public void Update(Movie entity)
        {
            _movieRepository.Update(entity);
        }
    }
}
