using AppData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("Details/{ID}")]
        public ActionResult BookDetails(string ID)
        {
            return Ok(_Context.Books.Find(ID));
        }
        [HttpPost("CreateNew")]
        public ActionResult BookCreate(Book NewBook)
        {
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
        [HttpPut("Edit/{ID}")]
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
        [HttpDelete("Delete/{ID}")]
        public ActionResult BookDelete(string ID)
        {
            try
            {
                var Target = _Context.Books.Find(ID);
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
    }
}
