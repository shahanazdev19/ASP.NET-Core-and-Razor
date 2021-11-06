using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookUserList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookUserList.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        public UpsertModel(ApplicationDBContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id==null)
            {
                //create
                return Page();
            }
            //update
            Book = await _db.Books.FirstOrDefaultAsync(u=>u.Id==id);
            if(Book==null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id==0)
                {
                    _db.Books.Add(Book);
                }
                else
                {
                    _db.Books.Update(Book);
                }
               
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return RedirectToPage();

        }
    }
}
