using AppData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class BillController : Controller
    {
        HttpClient Clyunt = new HttpClient();
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
            else return View();
        }

        public ActionResult YourBillDetails(Guid ID)
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Bill/YourBillDetails?ID={ID}";
                var Response = Clyunt.GetStringAsync(RequestURL).Result;
                List<BillDetails>? BillDetail = JsonConvert.DeserializeObject<List<BillDetails>>(Response);
                return View(BillDetail);
            }
            else return View();
        }

        public ActionResult CloneBill(Guid ID)
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists != null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Bill/CloneYourOldBill?ID={ID}&TargetUser={CheckIfSessionExists}";
                var ResponseGUID = new Guid(Clyunt.GetStringAsync(RequestURL).Result);
                return RedirectToAction(nameof(YourBillDetails), ResponseGUID);
            }
            else return View();
        }

        public ActionResult CancelBill(Guid ID)
        {
            string? CheckIfSessionExists = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionExists == null)
            {
                string RequestURL = $@"https://localhost:7029/BepisAPI/Bill/YourBillCancellation?ID={ID}";
                var Response = Clyunt.GetStringAsync(RequestURL).Result;
                return RedirectToAction(nameof(YourBills));
            }
            else return View();
        }
    }
}
