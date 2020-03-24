using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AspNetCore_04_Developing_Controllers.Models;
using AspNetCore_04_Developing_Controllers.Filters;

namespace AspNetCore_04_Developing_Controllers.Controllers
{
    public class CityController : Controller
    {
        #region Fields
        private IData _data;
        private IHostingEnvironment _enviroment;
        #endregion

        #region Constructor
        public CityController(IData data, IHostingEnvironment enviroment)
        {
            _data = data;
            _enviroment = enviroment;
            _data.CityInitializeData();
        }
        #endregion

        #region Methods
        [Route("WorldJourney")]
        [ServiceFilter(typeof(LogActionFilterAttribute))]
        public IActionResult Index()
        {
            ViewData["Page"] = "Search city";
            return View();
        }

        [Route("CityDetails/{id?}")]
        public IActionResult Details(int? id)
        {
            ViewData["Page"] = "Selected city";

            var city = _data.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }

            ViewBag.Title = city.CityName;
            return View(city);
        }

        public IActionResult GetImage(int? cityId)
        {
            ViewData["Message"] = "Display Image";

            City requestedCity = _data.GetCityById(cityId);

            if (requestedCity != null)
            {
                var webRootpath = _enviroment.WebRootPath;
                var folderPath = "\\images\\";
                var fullPath = webRootpath + folderPath + requestedCity.ImageName;

                var fileOnDisk = new FileStream(fullPath, FileMode.Open);
                byte[] fileBytes;

                using (var br = new BinaryReader(fileOnDisk))
                {
                    fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                }

                return File(fileBytes, requestedCity.ImageMimeType);
            }
            else
            {
                return NotFound();
            }
        }
        #endregion
    }
}