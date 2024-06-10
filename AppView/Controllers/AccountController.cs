using AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class AccountController : Controller
    {
        HttpClient Clyunt = new HttpClient();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            if (Username == null || Password == null)
            {
                TempData["NotificationFail"] = "Bạn điền thiếu thông tin!";
                return RedirectToAction("Login", "Account");
                // YOU ONLY NEED: 1 if statement if this crap is null or not.
            }
            using (AppDBContext Context = new AppDBContext())
            {
                var GetAccount = await Context.Accounts.FirstOrDefaultAsync(Find => Find.Username.Equals(Username));
                if (GetAccount != null && Password == GetAccount.Password)
                {
                    HttpContext.Session.SetString("NameUser", GetAccount.Username);
                    HttpContext.Session.SetInt32("PriorityPower_or_PP", GetAccount.Status);
                    // something like: 0 means normal user, 1 means mod, 2 means admin, 999 means owner, -1 means banned????
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["NotificationFail"] = "Sai thông tin đăng nhập!";
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        // GET: AccountController/Create
        public ActionResult Signup()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Account NewAccount)
        {
            try
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Account/CreateNew";
                var Response = Clyunt.PostAsJsonAsync(RequestURL, NewAccount).Result;
                return RedirectToAction("Login", "Account");
            }
            catch
            {
                return View();
            }
        }
    }
}
