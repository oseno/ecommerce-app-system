using ShaneCyber.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShaneCyber.Pages.Products
{
    //[Authorize(Roles = "Admin")]
    public class UpdateProductModel : PageModel
    {
        private readonly AppDataContext _db;
        public UpdateProductModel(AppDataContext db)
        {
            _db = db;
        }
        [BindProperty]
        public int ProductId { get; set; }
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public string ProductDescription { get; set; }
        [BindProperty]
        public decimal ProductPrice { get; set; }
        [BindProperty]
        public string ProductImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductId = product.ProductId;
            ProductName = product.ProductName;
            ProductDescription = product.ProductDescription;
            ProductPrice = product.ProductPrice;
            ProductImage = product.ProductImage;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productToUpdate = await _db.Products.FindAsync(ProductId);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.ProductName = ProductName;
            productToUpdate.ProductDescription = ProductDescription;
            productToUpdate.ProductPrice = ProductPrice;
            productToUpdate.ProductImage = ProductImage;


            await _db.SaveChangesAsync();

            return RedirectToPage("/Products/ViewProducts");

        }
    }
}