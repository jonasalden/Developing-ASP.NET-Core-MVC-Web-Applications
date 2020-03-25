using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore_06_Developing_Models.Models;
using AspNetCore_06_Developing_Models.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AspNetCore_06_Developing_Models.Controllers
{
    public class ButterflyController : Controller
    {
        #region Fields
        private IDataService _data;
        private IHostingEnvironment _environment;
        private IButterfliesQuantityService _butterfliesQuantityService;
        #endregion

        #region Constructor
        public ButterflyController(IDataService data, IHostingEnvironment environment, IButterfliesQuantityService butterfliesQuantityService)
        {
            _data = data;
            _environment = environment;
            _butterfliesQuantityService = butterfliesQuantityService;
            InitializeButterfliesData();
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Butterflies = _data.ButterfliesList
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Butterfly butterfly)
        {
            if (ModelState.IsValid)
            {
                var lastButterfly = _data.ButterfliesList.LastOrDefault();
                butterfly.CreatedDate = DateTime.Today;

                if (butterfly.PhotoAvatar != null && butterfly.PhotoAvatar.Length > 0)
                {
                    butterfly.ImageMimeType = butterfly.PhotoAvatar.ContentType;
                    butterfly.ImageName = Path.GetFileName(butterfly.PhotoAvatar.FileName);
                    butterfly.Id = lastButterfly.Id + 1;
                    _butterfliesQuantityService.AddButterfliesQuantityData(butterfly);

                    using (var memoryStream = new MemoryStream())
                    {
                        butterfly.PhotoAvatar.CopyTo(memoryStream);
                        butterfly.Photofile = memoryStream.ToArray();
                    }
                    _data.AddButterfly(butterfly);
                    return RedirectToAction("Index");
                }
            }
            return View(butterfly);
        }
        #endregion

        #region Helpers
        private void InitializeButterfliesData()
        {
            if (_data.ButterfliesList == null)
            {
                List<Butterfly> butterflies = _data.ButterfliesInitializeData();
                foreach (var butterfly in butterflies)
                {
                    _butterfliesQuantityService.AddButterfliesQuantityData(butterfly);
                }
            }
        }

        public IActionResult GetImage(int id)
        {
            Butterfly requestedButterfly = _data.GetButterflyById(id);

            if (requestedButterfly != null)
            {
                var webRootpath = _environment.WebRootPath;
                var folderPath = "\\images\\";
                var fullPath = webRootpath + folderPath + requestedButterfly.ImageName;

                if (System.IO.File.Exists(fullPath))
                {
                    var fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;

                    using (var br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, requestedButterfly.ImageMimeType);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
        #endregion
    }
}