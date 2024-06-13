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
            return Ok(_Context.BillDetails.Include(ProductP => ProductP.Bill).Include(ProductB => ProductB.Book).Where(Property => Property.BillID == ID).ToList());
        }

        // POST: Create a cart based on bill
        [HttpGet("CloneYourOldBill")]
        public ActionResult CloneYourOldBill(Guid ID, string TargetUser)
        {
            Guid DetailGUID = Guid.NewGuid();
            var CartUser = _Context.Carts.FirstOrDefault(Property => Property.Username == TargetUser);
            var CurrentCartContent = _Context.CartsDetails.Where(U => U.Username == TargetUser);
            var BillContent = _Context.BillDetails.Include(ProductP => ProductP.Bill).Include(ProductB => ProductB.Book).Where(Property => Property.BillID == ID).ToList();
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
                        if (CurrentCartContent != null)
                        {
                            foreach (var CartItem_Sub in CurrentCartContent)
                            {
                                if (CartItem_Sub.Quantity > 0)
                                {
                                    CartItem_Sub.Quantity += CartItem.Quantity;
                                }
                            }
                        }
                        else
                        {
                            CartDetails Details = new()
                            {
                                CartDetailsID = DetailGUID,
                                Username = TargetUser,
                                BookID = CartItem.BookID,
                                Quantity = CartItem.Quantity,
                                Status = 1
                            };
                            _Context.CartsDetails.Add(Details);
                        }
                        _Context.SaveChanges();
                    }
                }
            }
            return Ok(DetailGUID);
        }

        // PUT: Cancel the bill
        [HttpGet("YourBillCancellation")]
        public ActionResult YourBillCancellation(Guid ID)
        {
            var GetBill = _Context.Bills.Find(ID);
            var BillContent = _Context.BillDetails.Include(ProductP => ProductP.Bill).Include(ProductB => ProductB.Book).Where(Property => Property.BillID == ID).ToList();
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
                return BadRequest("Bill already cancelled or doesn't exist!\nPerhaps you were trying to get the response URL though, so whatever......!?");
            }
            _Context.SaveChanges();
            return Ok();
        }
    }
}
