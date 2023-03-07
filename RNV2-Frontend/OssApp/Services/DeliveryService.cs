using OssApp.Model;
using RestaurantDaoBase.Enums;
using System.Text;
using System.Text.Json;

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
        public string Assign(DeliveryModel model,string userId,string deliveryMan)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id", model.Id);
            dict.Add("DeliveryMan", deliveryMan);
            dict.Add("CreateBy", userId);
            dict.Add("EstimateTime", DateTime.Now.AddMinutes(30));
            dict.Add("CreateTime", DateTime.Now);

            string jsonString = JsonSerializer.Serialize(dict);
            return base.UpdateOne($"{BaseUrl}/Assign", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
    }
}
