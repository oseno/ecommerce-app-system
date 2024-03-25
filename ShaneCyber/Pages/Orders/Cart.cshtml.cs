using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShaneCyber.Models;

namespace ShaneCyber.Pages.Orders
{
    public class CartModel : PageModel
    {
        private readonly AppDataContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected static int orderId = 1;
        public static List<Order> Orders { get; set; }
        private decimal _orderTotal = 0;
        public decimal OrderTotal { get { return _orderTotal; } set { _orderTotal = value; } }

        public CartModel(AppDataContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;

        }
            
        public void OnGet(int id)
        {
            Orders = HttpContext.Session.GetObject<List<Order>>("Cart") ?? new List<Order>();

            var allProducts = _db.Products.Find(id);
            if (allProducts != null)
            {


                var product = Orders.Find(item => item.ProductId == allProducts.ProductId);
                if (product != null)
                {
                    product.OrderQuantity++; 
                    CalculateSubtotal();
                }
                else
                {
                    Orders.Add(new Order
                    {
                        OrderId= orderId++,
                        ProductId = allProducts.ProductId,
                        ProductName = allProducts.ProductName,
                        ProductPrice = allProducts.ProductPrice,
                        OrderQuantity = 1
                    });;
                }


                // Saving cart items to session
                HttpContext.Session.SetObject("Cart", Orders);

                // Calculate subtotal
                CalculateSubtotal();

            }
        }
        public IActionResult OnPostRemove(int id)
        {
            Orders = HttpContext.Session.GetObject<List<Order>>("Cart") ?? new List<Order>();
            var itemToRemove = Orders.Find(item => item.ProductId == id);

            if (itemToRemove != null)
            {
                Orders.Remove(itemToRemove);
                HttpContext.Session.SetObject("Cart", Orders); // Updating session with updated cart
                CalculateSubtotal();
            }

            return RedirectToPage();
        }

        private void CalculateSubtotal()
        {
            foreach (var item in Orders)
            {
                OrderTotal += item.ProductPrice * item.OrderQuantity;
            }
        }
    }
}



public static class SessionExtensions
{
    public static void SetObject<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}
