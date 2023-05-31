using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.OrderService
{
    public interface IOrderPhotoService
    {
        Task AddPhotoToOrder(OrderPhoto orderPhoto);
        Task DeletePhotoToOrder(OrderPhoto orderPhoto);
        List<OrderPhoto> GetAllPhotos();

    }
}