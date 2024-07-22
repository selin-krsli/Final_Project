using MovieApp.ENTITY;

namespace MovieApp.DATA.Abstract
{
    public interface IMovieRepository:IRepository<Movie>
    {
        List<Movie> GetSearchResult(string searchingWord);
        List<Movie> GetMovieWithCategories(string categoryName,int page,int pageSize);//movie info & which category
        Movie GetMovieDetails(string url);
        int GetMovieByCategory(string category);
        List<Movie> GetHomePageMovies();
    }
}
