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
        /// <summary>
        ///OrderByDescending bruges til at hente den nyeste Order Id som er blevet oprettet. 
        ///Grunden er for at kunne routere den nyeste Order Id videre nu man lave en OrderPhoto. 
        /// </summary>
        /// <param name="id">Booking Id</param>
        /// <returns> returende den order som er lige blevet oprettet</returns>
        public async Task<Order> GetOrderWithBookingById(int id)
        {
            using( var context = new ObjectDbContext())
            {
                 return context.Orders.Include(o => o.Booking).
                    ThenInclude(u => u.User).Where(b => b.BookingId == id).
                           AsNoTracking()
                            .OrderByDescending( o => o.CreatedDate).FirstOrDefault();  
            }
        } 
        /// <summary>
        /// Metoden skal hente Alle Order på den bruger som er logget.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns> returende en liste over Order på en spesifike User</returns>
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
        //Ikke brug
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

        /// <summary>
        ///  Den stater en automatiske slettning for hvis kunden ikke genemmføre sin order så vil sletter Order fra DB efter et bestemt tidspunt som vi kan vægle det. 
        /// </summary>
        public async Task StartAutoDeletionAsync(int orderId)
        {
            _timer = new Timer(state => DeleteIfConditionIsFalse(orderId), null, TimeSpan.FromMinutes(15), Timeout.InfiniteTimeSpan);

           // await Task.Delay(TimeSpan.FromMinutes(1));
           //var order = GetOrderById(orderId);
           //order.Condition  = true;
        }
        /// <summary>
        ///  Henter data fra DB og tjekke om Status på den efter den bestemt tidspunkt hvis den flase så slettes fra DB
        /// </summary>
        /// <param name="orderId"></param>
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
