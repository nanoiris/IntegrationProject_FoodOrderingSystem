using OssApp.Model;
using System.Text;
using System.Text.Json;

namespace OssApp.Services
{
    public class OrderService : RestService<OrderModel>
    {
        public static readonly string BaseUrl = "api/Order";
        public OrderService(string server) : base(server) { }

        public async Task<List<OrderModel>> Search(string customerName)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("email", customerName);
            string jsonString = JsonSerializer.Serialize(dict);
            return await base.List($"{BaseUrl}/SearchWithoutStatus", new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }

        public bool CancelOrder(OrderModel model)
        {
            return base.DeleteOne($"{BaseUrl}/CanceledOne/{model.Id}");
        }
    }
}
