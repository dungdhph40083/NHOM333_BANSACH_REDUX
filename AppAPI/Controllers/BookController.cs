using AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("BepisAPI/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        AppDBContext _Context = new AppDBContext();
        [HttpGet("List")]
        public ActionResult BookList()
        {
            return Ok(_Context.Books.ToList());
        }
        [HttpGet("Details")]
        public ActionResult BookDetails(string BookID)
        {
            return Ok(_Context.Books.Find(BookID));
        }
        [HttpPost("CreateNew")]
        public ActionResult BookCreate(Book NewBook)
        {
            NewBook.BillDetails = null;
            NewBook.CartDetails = null;
            try
            {
                _Context.Books.Add(NewBook);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("god dam mit dude you noclipped to the shadow realm");
            }
        }
        [HttpPut("Edit")]
        public ActionResult BookUpdate(Book CurrentBook)
        {
            try
            {
                var Target = _Context.Books.Find(CurrentBook.BookID);
                if (Target != null)
                {
                    Target.BookID = CurrentBook.BookID;
                    Target.Status = CurrentBook.Status;
                    Target.Author = CurrentBook.Author;
                    Target.Price = CurrentBook.Price;
                    Target.Amount = CurrentBook.Amount;
                    Target.Description = CurrentBook.Description;
                    return Ok();
                }
                else
                {
                    return RedirectToAction(nameof(BookList));
                }
            }
            catch (Exception)
            {
                return BadRequest("god dam mit dude you noclipped to the shadow realm");
            }
        }
        [HttpDelete("Delete")]
        public ActionResult BookDelete(string BookID)
        {
            try
            {
                var Target = _Context.Books.Find(BookID);
                if (Target != null)
                {
                    _Context.Books.Remove(Target);
                    _Context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return RedirectToAction(nameof(BookList));
                }
            }
            catch (Exception)
            {
                return BadRequest("god dam mit dude you noclipped to the shadow realm");
            }
        }
        [HttpPost("Add2Cart")]
        public ActionResult Add2Cart(string ID, int QuantityGet, string TargetUser)
        {
            QuantityGet = 1;
            var CartItem = _Context.CartsDetails.FirstOrDefault(Property => Property.BookID == ID && Property.Username == TargetUser);
            var CartUser = _Context.Carts.FirstOrDefault(Property => Property.Username == TargetUser);
            if (CartItem == null)
            {
                CartDetails Details = new()
                {
                    CartDetailsID = Guid.NewGuid(),
                    Username = TargetUser,
                    BookID = ID,
                    Quantity = QuantityGet,
                    Status = 1
                };
                _Context.CartsDetails.Add(Details);
                _Context.SaveChanges();
            }
            else
            {
                CartItem.Quantity += QuantityGet;
                _Context.CartsDetails.Update(CartItem);
                _Context.SaveChanges();
            }
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
            return Ok();
        }
    }
}
