using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.OrderService
{
    public interface IOrderPhotoService
    {
        Task AddPhotoToOrder(OrderPhoto orderPhoto);
        List<OrderPhoto> GetAllPhotos();
        decimal CalculatePrice(string size, int quantity);
        Task<OrderPhoto> RemovePhotoFromList(int photoIdInOrderList);
        decimal SumOfCalculatePrice(int orderid);
        List<OrderPhoto> BasketClean(int orderid);



    }
}