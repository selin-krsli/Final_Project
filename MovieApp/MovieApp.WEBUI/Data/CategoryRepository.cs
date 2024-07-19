using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.Data
{
    public class CategoryRepository
    {
        public static List<Category> _categories = null;
        static CategoryRepository()
        {
            _categories = new List<Category>()
            {
                new Category() { CategoryId=1, Name= "Action Films", Url="action"},
                new Category() { CategoryId=2, Name= "Dramatic Films", Url="dramatic"},
                new Category() { CategoryId=3, Name= "History Films", Url="history"}
            };
        }
        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
        public static void AddCategory(Category category)
        {
            _categories.Add(category);
        }
        public static Category GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(c=>c.CategoryId==id);
        }
    }
}
