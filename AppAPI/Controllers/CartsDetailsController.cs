using AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("BepisAPI/[controller]")]
    [ApiController]
    public class CartsDetailsController : ControllerBase
    {
        AppDBContext _Context = new AppDBContext();
        [HttpGet("CreateBill")]
        public ActionResult CreateBill(string TargetUser)
        {
            Guid BillGUID = Guid.NewGuid();
            var CartItems = _Context.CartsDetails
                                    .Include(PP => PP.Book)
                                    .Where(Name => Name.Username == TargetUser);
            if (CartItems != null)
            {
                foreach (var Item in CartItems)
                {
                    if (Item != null)
                    {
                        if (Item.Book.Amount - Item.Quantity >= 0)
                        {
                            Item.Book.Amount -= Item.Quantity;
                            _Context.Books.Update(Item.Book);
                            return Ok();
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                }
                foreach (var Item in CartItems)
                {
                    BillDetails Details = new()
                    {
                        BillDetailsID = Guid.NewGuid(),
                        BillID = BillGUID,
                        BookID = Item.BookID,
                        Quantity = Item.Quantity,
                        Price = Item.Book.Price
                    };
                    _Context.BillDetails.Add(Details);
                    _Context.CartsDetails.Remove(Item);
                }
                Bill NewBill = new()
                {
                    BillID = BillGUID,
                    Description = "Từ giỏ hàng",
                    CreationTime = DateTime.Now,
                    Username = TargetUser,
                    Status = 1
                };
                _Context.Bills.Add(NewBill);
                _Context.SaveChanges();
                return Ok(BillGUID);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(Guid Target)
        {
            var CartItem = _Context.CartsDetails.Find(Target);
            if (CartItem != null)
            {
                _Context.CartsDetails.Remove(CartItem);
            }
            _Context.SaveChanges();
            return Ok();
        }

        [HttpGet("CartDetails")]
        public ActionResult Details(string TargetUser)
        {
            if (TargetUser != null)
            {
                var CartItems = _Context.CartsDetails
                .Include(ProductP => ProductP.Book)
                .Where(Property => Property.Username == TargetUser);
                return Ok(CartItems);
            }
            else
            {
                return BadRequest(); 
            }
        }
    }
}
