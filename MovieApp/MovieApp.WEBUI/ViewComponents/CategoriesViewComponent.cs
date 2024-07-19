using Microsoft.AspNetCore.Mvc;
using MovieApp.WEBUI.Data;
using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View(CategoryRepository.Categories);
        }
    }
}
