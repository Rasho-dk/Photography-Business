using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.EFDbContext;
using PhotographyBusiness.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace PhotographyBusiness.Services.OrderService
{
   
    public class OrderService
    {
        private Timer _timer;
        private List<Order> OrderList;
        private GenericDbService<Order> genericDbService;
        public OrderService(GenericDbService<Order> genericDbService) 
        {
            this.genericDbService = genericDbService;
            //OrderList = new List<Order>();  
            OrderList = genericDbService.GetObjectsAsync().Result.ToList();
        }
        public async Task CreateOrder(Order order)
        {
            OrderList.Add(order);   
            await genericDbService.AddObjectAsync(order);
        }
        public async Task<List<Order>> GetAllOrders() 
        {
            return OrderList;
        }
        public async Task<Order> GetOrderByBookingIDAsync(int id)
        {
            foreach(var order in OrderList)
            {
                if(order.BookingId == id)
                {
                    return order;
                }
            }
            return null;
        }
        public async Task<Order> GetOrderWithBookingById(int id)
        {
            using( var context = new ObjectDbContext())
            {
                 return context.Orders.Include(o => o.Booking).
                    ThenInclude(u => u.User).Where(b => b.BookingId == id).
                           AsNoTracking()
                            .OrderByDescending( o => o.CreatedDate).FirstOrDefault();  

                //return context.Orders.Include(o => o.Booking)
                //       .ThenInclude(u => u.User).AsNoTracking().
                //       FirstOrDefault(u => u.BookingId == id);
            }
        } 
        public Task<List<Order>> GetOrderWithBookingByUserId(int id)
        {
            using(var context = new ObjectDbContext())
            {
                return Task.FromResult(context.Orders.Include(o => o.Booking).ThenInclude(u => u.User).Where(o => o.Booking.UserId.Equals(id)).AsNoTracking().ToList());
               
            }
        }
        public Order GetOrderById(int id)
        {
            foreach(var order in OrderList)
            {
                if (order.Id.Equals(id))
                {
                    return order;
                }
            }
            return null;
        }
        public async Task<Order> DeleteOrderAsync(int id)
        {
            Order orderToBeDeleted = null;
            foreach (var order in OrderList)
            {
                if (order.Id.Equals(id))
                {
                    orderToBeDeleted = order;
                    break;
                }
            }
            if (orderToBeDeleted != null)
            {
                OrderList.Remove(orderToBeDeleted);
                await genericDbService.DeleteObjectAsync(orderToBeDeleted);
            }
            return orderToBeDeleted;
        }
        public async Task StartAutoDeletionAsync(int orderId)
        {
            _timer = new Timer(state => DeleteIfConditionIsFalse(orderId), null, TimeSpan.FromMinutes(5), Timeout.InfiniteTimeSpan);

           // await Task.Delay(TimeSpan.FromMinutes(1));
           //var order = GetOrderById(orderId);
           //order.Condition  = true;
        }
        private async void DeleteIfConditionIsFalse(int orderId)
        {
          var order = genericDbService.GetObjectByIdAsync(orderId).Result;

            if (!order.Condition)
            {
                var OrderToRemove = OrderList.FirstOrDefault(o => o.Id == orderId); 
                if(OrderToRemove != null)
                    OrderList.Remove(OrderToRemove);
                await genericDbService.DeleteObjectAsync(OrderToRemove);
            }
        }
        public async Task ConfirmOrder(Order order)
        {
            foreach (var ord in OrderList)
            {
                if(ord.Id.Equals(order.Id))
                {
                    ord.Condition = order.Condition;
                    ord.TotalPriceWithTax = order.TotalPriceWithTax;
                }
            }
            await genericDbService.UpdateObjectAsync(order);
        }
       

    }
}
