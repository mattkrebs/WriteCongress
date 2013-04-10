using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WriteCongress.Core;

namespace WriteCongress.Web.Models
{
    public class OrderViewModel
    {

        public decimal Total { get; set; }
        public string ItemTitle { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string PdfLink { get; set; }


        public OrderViewModel()
        {


        }


        public List<OrderViewModel> GetOrdersByUser(User user)
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            if (user != null && user.Orders.Count > 0)
            {
                foreach (Order order in user.Orders)
	            {
                    OrderViewModel ovm = new OrderViewModel()
                    {
                        ItemTitle = order.Name,
                        OrderDate = order.CreateDateUtc,
                        Total = order.OrderTotal.HasValue ? order.OrderTotal.Value : (decimal)0.00,
                        OrderStatus = order.OrderStatus.Name
                    };
                    orderViewModels.Add(ovm);
	            }
                
            }

            return orderViewModels;
        }

    }
}