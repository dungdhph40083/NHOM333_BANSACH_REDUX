using AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("BepisAPI/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AppDBContext _Context = new AppDBContext();
        [HttpGet("Login")]
        public ActionResult AccountLogin(/* string Username, string Password */)
        {
            //var LookFor = _Context.Accounts.Find(Username);
            //if (LookFor != null && Password == LookFor.Password)
            //{
            //    HttpContext.Session.SetString("NameUser", LookFor.Username);
            //    HttpContext.Session.SetInt32("PriorityPower_or_PP", LookFor.Status);
                return Ok();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
        }
        [HttpPost("CreateNew")]
        public ActionResult AccountSignup(Account NewAcc)
        {
            try
            {
                _Context.Accounts.Add(NewAcc);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("god dam mit dude you noclipped to the shadow realm");
            }
        }
    }
}
