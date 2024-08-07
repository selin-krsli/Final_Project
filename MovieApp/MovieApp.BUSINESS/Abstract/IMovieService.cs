﻿using MovieApp.ENTITY;
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
        void Update(Movie entity, int[] categoryIds);
        void Delete(Movie entity);
        Movie GetMovieDetails(string url);
        List<Movie> GetMovieWithCategories(string categoryName,int page,int pageSize);
        int GetMovieByCategory(string category);
        List<Movie> GetHomePageMovies();
        List<Movie> GetSearchResult(string searchingWord);
        Movie GetByIdWithCategories(int id);
    }
}
