using Microsoft.AspNetCore.Mvc;
using AspNetCore_10_TestingTroubleShooting.Models;
using AspNetCore_10_TestingTroubleShooting.Services;
using Microsoft.Extensions.Logging;

namespace AspNetCore_10_TestingTroubleShooting.Controllers
{
    public class ShirtController : Controller
    {
        #region Fields
        private readonly IShirtRepository _repository;
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public ShirtController(IShirtRepository repository, ILogger<ShirtController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            var shirts = _repository.GetShirts();

            return View(shirts);
        }

        public IActionResult AddShirt(Shirt shirt)
        {
            _repository.AddShirt(shirt);
            _logger.LogDebug($"A {shirt.Color.ToString()} shirt of size {shirt.Size.ToString()} with a price of {shirt.GetFormattedTaxedPrice()} was added successfully.");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogDebug($"A shirt with id {id} was removed successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex + $"An error occured while trying to delete shirt with id of {id}.");
                throw ex;
            }
        }
        #endregion
    }
}