using Microsoft.AspNetCore.SignalR;

namespace ShaneCyber.Models
{
    public class Order
    {
        private int _orderQuantity = 0;
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int OrderQuantity { get { return _orderQuantity; } set { _orderQuantity = value; } }
        public decimal OrderTotal { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerAddress { get; set; }
    }

}
