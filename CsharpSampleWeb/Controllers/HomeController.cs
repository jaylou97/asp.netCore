using Microsoft.AspNetCore.Mvc;
using CsharpSampleWeb.Models;
using System.Diagnostics;

namespace CsharpSampleWeb.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/

        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        private readonly DatabaseContext _ctx;

        public HomeController(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // it is a get method
        public IActionResult RegistrationForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegistrationForm(Person person)
        {
            if (!ModelState.IsValid) { 
                return View();
            }

            try
            {
                _ctx.Person.Add(person);
                _ctx.SaveChanges();
                TempData["msg"] = "Submitted";
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }
        public IActionResult PeopleList()
        {
            //var person = _ctx.Person.ToList();
            //return View(person);*/
            var people = _ctx.Person.ToList(); // Fetch the list of people from your data source
            // Fetch a single person (adjust your code to fetch the desired person)
            var singlePerson = _ctx.Person.FirstOrDefault();

            var model = new MyCombinedModel
            {
                People = people,
                SinglePerson = singlePerson
            };
            return View(model);
        }
        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person = _ctx.Person.Find(id);
                if (person != null)
                {
                    _ctx.Person.Remove(person);
                    _ctx.SaveChanges();
                    return Json(new { success = true, message = "Deleted" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            // If the deletion failed, return a JSON response indicating failure
            return Json(new { success = false, message = "Deletion failed" });
        }
        public IActionResult EditForm(int id)
        {
            var person = _ctx.Person.Find(id);
            try
            {
                if (person != null)
                {
                    return Json(new { success = true, data = person });
                }
            }
            catch (Exception ex){
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = false, message = "No Data" });
        }
        [HttpPost]
        public IActionResult EditForm(MyCombinedModel person)
        {
            // update function not working if this condition is not commented
            /*if (!ModelState.IsValid)
            {
                //return RedirectToAction("PeopleList");
                // Debugging: Check ModelState for errors
                //var errors = ModelState.Values.SelectMany(v => v.Errors);
                // Log or display the errors
                //return View();
            }*/

            try
            {
                _ctx.Person.Update(person.SinglePerson);
                _ctx.SaveChanges();
                TempData["updateMsg"] = 123;
            }
            catch (Exception ex)
            {
                TempData["updateMsg"] = ex.Message;
            }
            //return View();
            return RedirectToAction("PeopleList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}