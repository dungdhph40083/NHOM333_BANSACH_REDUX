using AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class BookController : Controller
    {
        HttpClient Clyunt = new HttpClient();
        public BookController()
        {

        }
        // GET: BookController
        [Route("Book")]
        public ActionResult List()
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                ViewData["Username"] = CheckIfSessionExists;
            }
            string RequestURL = @"https://localhost:7029/BepisAPI/Book/List";
            var Response = Clyunt.GetStringAsync(RequestURL).Result;
            List<Book>? Books = JsonConvert.DeserializeObject<List<Book>>(Response);
            return View(Books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(string ID)
        {
            string RequestURL = $@"https://localhost:7029/BepisAPI/Book/Details?BookID={ID}";
            Console.WriteLine($"URL: {RequestURL}");
            var Response = Clyunt.GetStringAsync(RequestURL).Result;
            Book? FoundBook = JsonConvert.DeserializeObject<Book>(Response);
            return View(FoundBook);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book NewBook)
        {
            try
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Book/CreateNew";
                var Response = Clyunt.PostAsJsonAsync(RequestURL, NewBook).Result;
                return RedirectToAction("List", "Book");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(string ID)
        {
            string RequestURL = $@"https://localhost:7029/BepisAPI/Book/Details?BookID={ID}";
            var Response = Clyunt.GetStringAsync(RequestURL).Result;
            Book? FoundBook = JsonConvert.DeserializeObject<Book>(Response);
            return View(FoundBook);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string ID, Book ExistingBook)
        {
            try
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Book/Edit?BookID={ID}";
                var Response = Clyunt.PutAsJsonAsync(RequestURL, ExistingBook).Result;
                return RedirectToAction("List", "Book");
            }
            catch
            {
                return View();
            }
        }
        // POST: BookController/Delete/5
        public ActionResult Delete(string ID)
        {
            try
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Book/Delete?BookID={ID}";
                var Response = Clyunt.DeleteAsync(RequestURL);
                return RedirectToAction("List", "Book");
            }
            catch
            {
                return RedirectToAction("List", "Book");
            }
        }

        public ActionResult Add2Cart(string ID, int QuantityGet)
        {
            try
            {
                string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
                if (CheckIfSessionExists != null)
                {
                    string RequestURL = $@"https://localhost:7029/BepisAPI/Book/Add2Cart?ID={ID}&QuantityGet={QuantityGet}&TargetUser={CheckIfSessionExists}";
                    Clyunt.PostAsync(RequestURL, null);
                    return RedirectToAction("YourCart", "CartsDetails");
                }
                else
                {
                    TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn! Hãy đăng nhập lại.";
                    return RedirectToAction("Login", "Account");
                }
            }
            catch
            {
                return RedirectToAction("List", "Book");
            }
        }
    }
}
