using MovieApp.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BUSINESS.Abstract
{
    public interface IMovieService
    {
        Movie GetById(int id);
        List<Movie> GetAll();
        void Create(Movie entity);
        void Update(Movie entity);
        void Delete(Movie entity);
        Movie GetMovieDetails(string url);
        List<Movie> GetMovieWithCategories(string categoryName);
    }
}
