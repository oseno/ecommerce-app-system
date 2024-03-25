using ShaneCyber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShaneCyber.Pages.Products
{
    public class ProductModel : PageModel
    {
        public readonly AppDataContext _db;

        public List<Product> ProductList { get; set; }

        public ProductModel(AppDataContext db)
        {
            _db = db;
        }
        [ActionName("ViewProducts")]
        public void OnGet()
        {
            ProductList = _db.Products.ToList();
        }
    }

}
