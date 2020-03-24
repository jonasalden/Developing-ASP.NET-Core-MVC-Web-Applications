using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_04_Developing_Controllers.Controllers
{
    public class HomeController : Controller
    {
        #region Methods
        public IActionResult Index()
        {
            return RedirectToAction("Index", "City");
        }
        #endregion
    }
}