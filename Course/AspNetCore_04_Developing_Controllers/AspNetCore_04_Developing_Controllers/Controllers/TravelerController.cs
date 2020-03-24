using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_04_Developing_Controllers.Controllers
{
    public class TravelerController : Controller
    {
        #region Methods
        public IActionResult Index(string name)
        {
            ViewBag.VisitorName = name;
            return View();
        }
        #endregion
    }
}