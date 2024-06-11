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
        [HttpPost("CreateNew")]
        public ActionResult AccountSignup(Account NewAcc)
        {
            NewAcc.Bills = null;
            NewAcc.Cart = null;
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
        [HttpGet("AccountList")]
        public ActionResult AccountList()
        {
            return Ok(_Context.Accounts.ToList());
        }
    }
}
