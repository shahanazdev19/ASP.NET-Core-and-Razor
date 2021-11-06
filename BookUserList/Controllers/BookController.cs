using BookUserList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookUserList.Controllers
{
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;
        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }


        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Books.FindAsync(id);
            if (bookFromDb == null)
            {
                return Json(new {success=false, message="Error while Deleting" });
            }
            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new {success=true, message="Delete successful" });
        }
    }
}
