using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShaneCyber.Models;

namespace ShaneCyber.Pages.Products
{
    //[Authorize(Roles = "Admin")]
    public class AddProductsModel : PageModel
    {
        public readonly AppDataContext _db;
        public AddProductsModel(AppDataContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Product product { get; set; }

        public IActionResult OnPost()
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return Redirect("./Product/ViewProducts");
        }
    }
}
