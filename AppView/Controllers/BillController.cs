using AppData;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class BillController : Controller
    {
        HttpClient Clyunt = new HttpClient();

        [HttpGet]
        public ActionResult YourBills()
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Bill/YourBillsList?Username={CheckIfSessionExists}";
                var Response = Clyunt.GetStringAsync(RequestURL).Result;
                List<Bill>? BillList = JsonConvert.DeserializeObject<List<Bill>>(Response);
                return View(BillList);
            }
            else
            {
                TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn! Hãy đăng nhập lại.";
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult YourBillDetails(Guid ID)
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Bill/YourBillDetails?ID={ID}";
                Console.WriteLine(RequestURL);
                var Response = Clyunt.GetStringAsync(RequestURL).Result;
                List<BillDetails>? BillDetail = JsonConvert.DeserializeObject<List<BillDetails>>(Response);
                ViewData["BillID"] = ID;
                return View(BillDetail);
            }
            else
            {
                TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn! Hãy đăng nhập lại.";
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult CloneBill(Guid ID)
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Bill/CloneYourOldBill?ID={ID}&TargetUser={CheckIfSessionExists}";
                var ResponseGUID = Clyunt.GetStringAsync(RequestURL).Result;
                return Redirect($"Bill/YourBillDetails/{ResponseGUID.TrimStart('\"').TrimEnd('\"')}");
            }
            else
            {
                TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn! Hãy đăng nhập lại.";
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPut]
        public ActionResult CancelBill(Guid ID)
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists == null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Bill/YourBillCancellation?ID={ID}";
                var Response = Clyunt.PutAsync(RequestURL, null).Result;
                return RedirectToAction(nameof(YourBills));
            }
            else
            {
                TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn! Hãy đăng nhập lại.";
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
