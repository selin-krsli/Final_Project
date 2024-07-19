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
        public List<Category> GetPopularCategories()
        {
            using (var context = new MovieContext())
            {
                return context.Categories.ToList();
            }
        }
    }
}
