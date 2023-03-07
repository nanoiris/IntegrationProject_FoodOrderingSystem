using RestaurantNetowrkApp.Data.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Serilog;
using RestaurantNetowrkApp.Data.Dto;
using Microsoft.JSInterop;
using System.Text.Json;
using Stripe.Checkout;
using Stripe;

namespace RestaurantNetowrkApp.Services
{
    public class OrderService
    {
        internal HttpClient http;
        public string server;
        public enum StatusEnum
        {
            Cart = 0, Paid = 1, Accepted = 2, Ready = 3, Completed = 4, Canceled = 5
        }

        public OrderService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }

        public async Task<bool> createCart(CartModel cart)
        {
            var response = await http.PostAsJsonAsync("api/cart/newone", cart);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            Log.Debug("Create new cart failed,try again");
            return false;
        }

        public async Task<OrderDto> getCart(string userEmail, string restId)
        {
            var requestBody =
                new
                {
                    email = userEmail,
                    status = 0,
                    restaurantId = restId
                };
            HttpResponseMessage response = await http.PostAsJsonAsync("api/order/search", requestBody);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var orders = JsonSerializer.Deserialize<List<OrderDto>>(responseBody);
            if (orders.Count != 0)
            {
                return orders[0];
            }
            return null;
        }

        public async Task<bool> addToCart(MenuItemDto menuItem, OrderDto orderCart)
        {
            var orderItem = new AddToCartModel
            {
                id = menuItem.Id,
                name = menuItem.Name,
                description = menuItem.Description,
                isFeatured = menuItem.IsFeatured,
                price = menuItem.Price,
                logo = menuItem.Logo,
                categoryId = menuItem.CategoryId,
            };
            var response = await http.PutAsJsonAsync($"api/cart/dishaddedtocart/{orderCart.Id}", orderItem);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            Log.Debug("Add to cart failed,try again");
            return false;
        }

        public async Task<OrderItemDto> increase(string menuItemId, OrderDto orderCart)
        {
            OrderItemDto orderItem = new OrderItemDto();           
            List<OrderItemDto> orderItems = orderCart.ItemList;
            orderItem = orderItems.FirstOrDefault(x => x.Item.Id == menuItemId);
            string orderItemId = orderItem.Id;
            string orderId = orderCart.Id;
            await increaseOrderItem(orderItemId, orderId);
            return orderItem;
        }

        public async Task<bool> increaseOrderItem(string orderItemId, string orderId)
        {
            var response = await http.PutAsync($"api/cart/increaseqty/{orderId}/{orderItemId}", null);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        public async Task<OrderItemDto> decrease(string menuItemId, OrderDto orderCart)
        {
            OrderItemDto orderItem = new OrderItemDto();          
            List<OrderItemDto> orderItems = orderCart.ItemList;
            orderItem = orderItems.FirstOrDefault(x => x.Item.Id == menuItemId);
            string orderItemId = orderItem.Id;
            string orderId = orderCart.Id;
            await decreaseOrderItem(orderItemId, orderId);
            return orderItem;
        }
        public async Task<bool> decreaseOrderItem(string orderItemId, string orderId)
        {
            var response = await http.PutAsync($"api/cart/decreaseqty/{orderId}/{orderItemId}", null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        public OrderDto getOrderById(string id)
        {
            return http.GetFromJsonAsync<OrderDto>($"api/order/one/{id}").Result;
        }

        public async Task<List<OrderDto>> getOrderByStatus(string userEmail, int status)
        {
            var requestBody = new
            {
                userName = userEmail,
                status = status,
            };
            var response = await http.PostAsJsonAsync("api/order/ListByUserAndStatus", requestBody);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<OrderDto>>().Result;
            }
            return null;
        }

        public async Task<bool> cancleOrder(string orderId)
        {
            var response = await http.DeleteAsync($"api/order/CanceledOne/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        } 


        public async Task<string> pay(OrderDto order, CardInfo cardInfo)
        {
            string Msg = "Checkout faild";
            try
            {
                long amount = (long)(order.PayTotal * 100);
                string email = order.UserName;
                string description = $"Order from {order.RestaurantName}";
                Stripe.StripeConfiguration.ApiKey = Data.Constants.keySecret;

                string[] strs = cardInfo.ValidThrough.Split("/");
                var optionToken = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = cardInfo.CardNumber,
                        ExpMonth = strs[0],
                        ExpYear = strs[1],
                        Cvc = cardInfo.Cvv
                    }
                };

                var serviceToken = new TokenService();
                Token stripetoken = await serviceToken.CreateAsync(optionToken);

                var customer = new Stripe.Customer
                {
                    Name = "Jack",
                    Address = new Address
                    {
                        Country = "canada",
                        City = "Montreal",
                        Line1 = "13 sr",
                        PostalCode = "234455"
                    }
                };

                var options = new Stripe.ChargeCreateOptions
                {
                    Amount = (long)(order.PayTotal *100),
                    Currency = "cad",
                    Description= description,
                    ReceiptEmail= email,
                    Source = stripetoken.Id,
                };
                var service = new Stripe.ChargeService();
                Stripe.Charge charge =await  service.CreateAsync(options);
                Msg = charge.Status;
                if (charge.Paid)
                {
                    return "Success";
                }
                else
                {
                    return "fail";
                }
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
                throw ex;
            }

        }

        public async Task<bool> changeOrderStatus(string orderId, int status)
        {
            var requestBody = new
            {
                OrderId = orderId,
                Status = status
            };
            var response =await http.PutAsJsonAsync("api/Order/OrderStatus", requestBody);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


    }
}
