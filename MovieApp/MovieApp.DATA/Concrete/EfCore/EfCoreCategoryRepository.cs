using Microsoft.EntityFrameworkCore;
using MovieApp.DATA.Abstract;
using MovieApp.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DATA.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, MovieContext>, ICategoryRepository
    {
        public Category GetByIdWithMovies(int categoryId)
        {
            using(var context = new MovieContext())
            {
                return context.Categories
                               .Where(s => s.CategoryId == categoryId)
                               .Include(s => s.MovieCategories)
                               .ThenInclude(s => s.Movie)
                               .FirstOrDefault();
            }
        }

    }
}
