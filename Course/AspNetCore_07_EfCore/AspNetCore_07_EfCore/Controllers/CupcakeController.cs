using AspNetCore_07_EfCore.Models;
using AspNetCore_07_EfCore.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCore_07_EfCore.Controllers
{
    public class CupcakeController : Controller
    {
        #region Fields
        private ICupcakeRepository _repository;
        private IHostingEnvironment _environment;
        #endregion

        #region Constructor
        public CupcakeController(ICupcakeRepository repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View(_repository.GetCupCakes());
        }

        public IActionResult Details(int id)
        {
            var cupCake = _repository.GetCupCakeById(id);

            if (cupCake == null)
            {
                return NotFound();
            }

            return View(cupCake);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateBakeriesDropDownList(ViewBag.BakeryId);
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(CupCake cupCake)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateCupCake(cupCake);
                return RedirectToAction(nameof(Index));
            }
            PopulateBakeriesDropDownList(ViewBag.BakeryId);

            return View(cupCake);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cupCake = _repository.GetCupCakeById(id);

            if (cupCake == null)
            {
                return NotFound();
            }

            PopulateBakeriesDropDownList(ViewBag.BakeryId);

            return View(cupCake);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPostAsync(int id)
        {
            var cupCakeToUpdate = _repository.GetCupCakeById(id);

            var isUpdated = await TryUpdateModelAsync(cupCakeToUpdate, "", c => c.BakeryId, c => c.CupcakeType, c => c.Description, c => c.GlutenFree, c => c.Price);

            if (isUpdated)
            {
                _repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            PopulateBakeriesDropDownList(ViewBag.BakeryId);

            return View(cupCakeToUpdate);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cupCake = _repository.GetCupCakeById(id);

            if (cupCake == null)
            {
                return NotFound();
            }

            return View(cupCake);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteCupCake(id);

            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Helpers
        public IActionResult GetImage(int id)
        {
            CupCake requestedCupcake = _repository.GetCupCakeById(id);
            if (requestedCupcake != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\images\\";
                string fullPath = webRootpath + folderPath + requestedCupcake.ImageName;
                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, requestedCupcake.ImageMimeType);
                }
                else
                {
                    if (requestedCupcake.PhotoFile.Length > 0)
                    {
                        return File(requestedCupcake.PhotoFile, requestedCupcake.ImageMimeType);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }

        private void PopulateBakeriesDropDownList(int? selectedBakery)
        {
            var bakeries = _repository.PopulateBakeriesDropDownList();

            ViewBag.BakeryID = new SelectList(bakeries.AsNoTracking(), "BakeryId", "BakeryName", selectedBakery);
        }
        #endregion
    }
}