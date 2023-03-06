using OssApp.Model;
using RestaurantDaoBase.Enums;

namespace OssApp.Services
{
    public class DeliveryService : RestService<DeliveryModel>
    {
        public static readonly string BaseUrl = "api/Delivery";
        public DeliveryService(string server) : base(server) { }

        public async Task<List<DeliveryModel>> ListActive()
        {
            return await base.List($"{BaseUrl}/ActiveList");
        }

        public string ChangeStatus2Pending(DeliveryModel model)
        {
            return base.UpdateOne($"{BaseUrl}/DeliveryStatus/{model.Id}/{DeliveryStatusEnum.Pending}",null);
        }
    }
}
