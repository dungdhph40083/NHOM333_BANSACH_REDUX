using AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class AccountController : Controller
    {
        HttpClient Clyunt = new HttpClient();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username, string Password)
        {
            if (Username == null || Password == null)
            {
                TempData["NotificationFail"] = "Bạn điền thiếu thông tin!";
                return RedirectToAction("Login", "Account");
                // YOU ONLY NEED: 1 if statement if this crap is null or not.
            }
            string RequestURL = $@"https://localhost:7029/BepisAPI/Account/AccountLogin?Username={Username}&Password={Password}";
                string? Response = Clyunt.GetStringAsync(RequestURL).Result;
                if (Response != null)
                {
                    HttpContext.Session.SetString("NameUser", Response);
                    // something like: 0 means normal user, 1 means mod, 2 means admin, 999 means owner, -1 means banned????
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["NotificationFail"] = "Sai thông tin đăng nhập!";
                    return RedirectToAction("Login", "Account");
                }
        }

        // GET: AccountController/Create
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: AccountController/Create
        public ActionResult Signup(Account NewAccount)
        {
            try
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Account/CreateNew";
                var Response = Clyunt.PostAsJsonAsync(RequestURL, NewAccount).Result;
                if (Response != null)
                {
                    TempData["NotificationSuccess"] = "Thành công! Hãy đăng nhập.";
                }
                else
                {
                    TempData["NotificationFail"] = "Đã có lỗi xảy ra!";
                }
                return RedirectToAction("Login", "Account");
            }
            catch
            {
                return View();
            }
        }
    }
}
