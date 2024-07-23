using MovieApp.ENTITY;

namespace MovieApp.BUSINESS.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);
        List<Category> GetAll();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        Category GetByIdWithMovies(int categoryId);
    }
}
