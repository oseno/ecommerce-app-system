using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using ShaneCyber.Models;

namespace ShaneCyber.Pages.Orders
{
    public class CheckoutModel : PageModel
    {
        private readonly AppDataContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected static int orderId = 1;
        public static List<Order> Orders { get; set; }
        public Order order { get; set; }
        private decimal _orderTotal = 0;
        private decimal _completeOrderTotal = 0;
        public decimal CompleteOrderTotal { get { return _completeOrderTotal; } set { _completeOrderTotal = value; } }
        public decimal OrderTotal { get { return _orderTotal; } set { _orderTotal = value; } }

        public CheckoutModel(AppDataContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;

        }

        public void OnGet(int id)
        {
            var allProducts = _db.Products.Find(id);
            if (allProducts != null)
            {


                var product = Orders.Find(item => item.ProductId == allProducts.ProductId);
                if (product != null)
                {
                    product.OrderQuantity++;
                    CalculateOrderTotal();
                }
                else
                {
                    Orders.Add(new Order
                    {
                        OrderId = orderId++,
                        ProductId = allProducts.ProductId,
                        ProductName = allProducts.ProductName,
                        ProductPrice = allProducts.ProductPrice,
                        OrderQuantity = 1
                    }); ;
                }

                // Calculate subtotal
                CalculateOrderTotal();
            }
        }
        public IActionResult OnPost()
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            return Redirect("./Order/CheckoutSuccess");
        }
        private void CalculateOrderTotal()
        {
            foreach (var item in Orders)
            {
                CompleteOrderTotal = item.OrderTotal++;
            }
        }
    }
}








