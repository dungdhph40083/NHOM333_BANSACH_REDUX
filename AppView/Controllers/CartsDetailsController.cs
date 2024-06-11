using AppData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class CartsDetailsController : Controller
    {
        HttpClient Clyunt = new HttpClient();
        public ActionResult YourCart()
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/CartsDetails/CartDetails?TargetUser={CheckIfSessionExists}";
                var Response = Clyunt.GetStringAsync(RequestURL).Result;
                List<CartDetails>? CartDetail = JsonConvert.DeserializeObject<List<CartDetails>>(Response);
                return View(CartDetail);
            }
            else return View();
        }
        public ActionResult DeleteItem(Guid TargetID)
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/CartsDetails/Delete?Target={TargetID}";
                var Response = Clyunt.DeleteAsync(RequestURL);
                return RedirectToAction(nameof(YourCart));
            }
            else return View();
        }

        public ActionResult CreateBill()
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/CartsDetails/CreateBill?TargetUser={CheckIfSessionExists}";
                var Response = Clyunt.GetStringAsync(RequestURL).Result;
                return RedirectToAction("BillDetails", "Bill", Response);
            }
            else return View();
        }
    }
}
