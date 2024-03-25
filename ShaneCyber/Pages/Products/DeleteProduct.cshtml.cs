using ShaneCyber.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShaneCyber.Pages.Products
{
    //[Authorize(Roles = "Admin")]
    public class DeleteProductModel : PageModel
    {
        private readonly AppDataContext _db;

        public DeleteProductModel(AppDataContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _db.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Product = await _db.Products.FindAsync(id);

            if (Product != null)
            {
                _db.Products.Remove(Product);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./ViewProducts");
        }
    }
}
