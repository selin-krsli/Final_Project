using Microsoft.AspNetCore.Mvc;


namespace MovieApp.WEBUI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View(/*CategoryRepository.Categories*/);
        }
    }
}
