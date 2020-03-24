using AspNetCore_05_Developing_Views.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_05_Developing_Views.Controllers
{
    public class CityController : Controller
    {
        #region Fields
        private readonly ICityProvider _cities;
        #endregion

        #region Constructor
        public CityController(ICityProvider cities)
        {
            _cities = cities;
        }
        #endregion

        #region Methods
        public IActionResult ShowCities()
        {
            ViewBag.Cities = _cities;
            return View();
        }

        public IActionResult ShowDataForCity(string cityName)
        {
            ViewBag.City = _cities[cityName];
            return View();
        }

        public IActionResult GetImage(string cityName)
        {
            return File($@"images\{cityName}.jpg", "image/jpeg");
        }
        #endregion
    }
}