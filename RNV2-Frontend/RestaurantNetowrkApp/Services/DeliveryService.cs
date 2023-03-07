using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using RestaurantDaoBase.Models;
using RestaurantNetowrkApp.Data.Dto;
using Serilog;

namespace RestaurantNetowrkApp.Services
{
    public class DeliveryService
    {
        internal HttpClient http;
        public string server;
        public DeliveryService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }

        public Delivery getDeliveryByOrderId(string orderId)
        {
            return http.GetFromJsonAsync<Delivery>($"api/Delivery/OneByOrderId/{orderId}").Result;
            
        }

        public async Task<bool> createDelivery(OrderDto order, RestaurantDto rest, Address address)
        {
            var requestBody = new Delivery
            {
                OrderId = order.Id,
                Restaurant = new RestAddress
                {
                    Id = rest.Id,
                    Name = rest.Name,
                    Address = rest.Address
                },
                CustomerName = order.UserName,
                Destination = address,
                CreateTime = DateTime.Now,
                Status = 0
            };
            var response = await http.PostAsJsonAsync("api/Delivery/NewOne", requestBody);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            Log.Debug("Create delivery failed,try again");
            return false;
        }
    }
}
