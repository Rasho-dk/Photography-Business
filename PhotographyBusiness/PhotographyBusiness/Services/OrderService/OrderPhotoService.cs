using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.OrderService
{
    public class OrderPhotoService : IOrderPhotoService
    {
        private List<OrderPhoto> _orderPhotos;
        public OrderPhotoService()
        {
            
        }

        public async Task AddPhotoToOrder(OrderPhoto orderPhoto)
        {
            _orderPhotos.Add(orderPhoto);
        }

        public Task DeletePhotoToOrder(OrderPhoto orderPhoto)
        {
            throw new NotImplementedException();
        }

        public List<OrderPhoto> GetAllPhotos()
        {
            return _orderPhotos;
        }
    }
}
