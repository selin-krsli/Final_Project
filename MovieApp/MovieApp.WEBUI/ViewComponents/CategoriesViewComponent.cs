using Microsoft.AspNetCore.Mvc;
using MovieApp.BUSINESS.Abstract;


namespace MovieApp.WEBUI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_categoryService.GetAll());
        }
    }
}
