using AppData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("BepisAPI/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        AppDBContext _Context = new AppDBContext();
        // GET: Bills
        [HttpGet("YourBillsList")]
        public ActionResult YourBillsList(string Username)
        {
            return Ok(_Context.Bills.Where(U => U.Username == Username).ToList());
        }

        // GET: Bill's details
        [HttpGet("YourBillDetails")]
        public ActionResult YourBillDetails(Guid ID)
        {
            return Ok(_Context.BillDetails
                     .Include(ProductP => ProductP.Bill)
                     .Include(ProductB => ProductB.Book)
                     .Where(Property => Property.BillID == ID));
        }

        // POST: Create a cart based on bill
        [HttpPost("CloneYourOldBill")]
        public ActionResult CloneYourOldBill(Guid ID, string TargetUser)
        {
            var CartUser = _Context.Carts.FirstOrDefault(Property => Property.Username == TargetUser);
            var BillContent = _Context.BillDetails
                 .Include(ProductP => ProductP.Bill)
                 .Include(ProductB => ProductB.Book)
                 .Where(Property => Property.BillID == ID);
            if (CartUser == null)
            {
                Cart NewCart = new()
                {
                    Username = TargetUser,
                    Status = 1
                };
                _Context.Carts.Add(NewCart);
                _Context.SaveChanges();
            }
            if (BillContent != null)
            {
                foreach (var CartItem in BillContent)
                {
                    if (CartItem != null)
                    {
                        CartDetails Details = new()
                        {
                            CartDetailsID = Guid.NewGuid(),
                            Username = TargetUser,
                            BookID = CartItem.BookID,
                            Quantity = CartItem.Quantity,
                            Status = 1
                        };
                        _Context.CartsDetails.Add(Details);
                        _Context.SaveChanges();
                    }
                }
            }
            return Ok();
        }

        // PUT: Cancel the bill
        [HttpPut("YourBillCancellation")]
        public ActionResult YourBillCancellation(Guid ID)
        {
            var GetBill = _Context.Bills.Find(ID);
            var BillContent = _Context.BillDetails
                             .Include(ProductP => ProductP.Bill)
                             .Include(ProductB => ProductB.Book)
                             .Where(Property => Property.BillID == ID);
            if (BillContent != null && GetBill != null && GetBill.Status != 100)
            {
                foreach (var Item in BillContent)
                {
                    var TargetBook = _Context.Books.Find(Item.BookID);
                    if (TargetBook != null)
                    {
                        TargetBook.Amount += Item.Quantity;
                        _Context.Books.Update(TargetBook);
                    }
                }
                GetBill.Status = 100;
                _Context.Bills.Update(GetBill);
            }
            else // Maybe I'll add a check that does something if the status quo is 100
            {
                return BadRequest();
            }
            _Context.SaveChanges();
            return Ok();
        }
    }
}
