using Microsoft.AspNetCore.Mvc;
using obilet_Assignment.Interface;
using obilet_Assignment.Models;
using System.Diagnostics;

namespace obilet_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IObiletBase _obiletBase;

        public HomeController(IObiletBase obiletBase)
        {
            _obiletBase = obiletBase;
        }

        public async Task<IActionResult> Index(bool isReturnPage)
        {
            string userAgent = Request.Headers["User-Agent"].ToString();
            if (string.IsNullOrEmpty(userAgent)) return View("Error");

            await _obiletBase.GetBrowserInformation(userAgent);

            IndexViewModel viewModel = await _obiletBase.GetBusLocations(isReturnPage);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetJourneys(IndexViewModel model)
        {
            await _obiletBase.SetJourneyDetailCache(model);

            if (ModelState.IsValid)
            {
                IndexViewJourneyModel viewModel = await _obiletBase.GetJourney(model);
                return View("JourneyIndex", viewModel);
            }

            else
            {
                IndexViewModel viewModel = await _obiletBase.GetBusLocations(true);
                return View("Index", viewModel);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}