using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.EFDbContext;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.OrderService
{
    public class OrderPhotoService : IOrderPhotoService
    {
        private const decimal firstCase = 100;
        private const decimal secondCase = 150;
        private const decimal thirdCase = 200;
        private const decimal fourthCase = 250;
        private const decimal fifthCase = 300;

        private List<OrderPhoto> _orderPhotos;
        private GenericDbService<OrderPhoto> _dbService;
        public OrderPhotoService(GenericDbService<OrderPhoto> dbService)
        {
            _dbService = dbService;
            _orderPhotos = _dbService.GetObjectsAsync().Result.ToList();
        }
        public async Task AddPhotoToOrder(OrderPhoto orderPhoto)
        {
            _orderPhotos.Add(orderPhoto);
           await _dbService.AddObjectAsync(orderPhoto);
        }
        public List<OrderPhoto> GetAllPhotos()
        {
            return _orderPhotos;
        }
        public decimal CalculatePrice(string size,int quantity)
        {
            decimal price = 0;
            switch (size)
            {
                case "4x6": return price = firstCase * quantity ;
                case "5x7": return price = secondCase * quantity ;
                case "8x8": return price = thirdCase * quantity ;
                case "10x12": return price = fourthCase * quantity ;
                case "20x24": return price = fifthCase * quantity ;

            }
            return price;   

        }

        public async Task<OrderPhoto> RemovePhotoFromList(int id)
        {
            foreach (OrderPhoto photo in _orderPhotos)
            {
                if(photo.Id == id)
                {
                    //_orderPhotos.Remove(photo);
                    await _dbService.DeleteObjectAsync(photo);  
                }
                
            }
            return null;
 
        }
        public decimal SumOfCalculatePrice(int orderid)
        {
            decimal price = 0;
            List<decimal> ints = new List<decimal>();
            foreach (OrderPhoto pric in _orderPhotos)
            {
               if(pric.OrderId == orderid)
               {
                    ints.Add(pric.Price);

               }
            }
            price = ints.Sum();
            return price;
        }
        public List<OrderPhoto> BasketClean(int orderid)
        {
            List<OrderPhoto> temp = new List<OrderPhoto>();
            foreach(OrderPhoto photo in _orderPhotos)
            {
                if(photo.OrderId == orderid)
                {
                    temp.Add(photo);
                }
            }
            temp.Clear();
            return _orderPhotos = temp ;
     
        }
    
    }
}
