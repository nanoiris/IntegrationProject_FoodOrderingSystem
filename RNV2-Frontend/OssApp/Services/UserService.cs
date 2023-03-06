using OssApp.Model;
using System.Net.Http.Json;
using Serilog;

namespace OssApp.Services
{
    public class UserService : RestService<AppUserModel>
    {
        public static readonly string BaseUrl = "api/OssUser";
        public string LogoUploadUrl { get; set; }
        public UserService(string server) : base(server) 
        {
            this.LogoUploadUrl = $"{server}/{BaseUrl}/Logo/";
        }
        
        public async Task<List<AppUserModel>> List(string restaurantId)
        {
            var result = await base.List($"{BaseUrl}/ByRestaurant/{restaurantId}");
            if(result == null)
            {
                return new List<AppUserModel>();
            }
            result.ForEach(row => {
                if (row.Logo != null)
                    row.Logo = Utils.BuildLogoPath(row.Logo);
                if (row.Status == 0)
                    row.IsActive = false;
                else row.IsActive = true;
            });
            return result;
        }

        public string AddNewOne(AppUserModel row)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Name), "Name");
            content.Add(new StringContent(row.Email), "Email");
            content.Add(new StringContent(row.PhoneNumber), "PhoneNo");
            content.Add(new StringContent(row.RestaurantId), "RestaurantId");
            content.Add(new StringContent(row.RestaurantName), "RestaurantName");

            if (row.IsActive == true)
            {
                content.Add(new StringContent("1"), "Status");
            }
            else
                content.Add(new StringContent("0"), "Status");

            return base.AddNewOne($"{BaseUrl}/NewOne",content);
        }

        public bool DeleteOne(AppUserModel row)
        {
            return base.DeleteOne($"{BaseUrl}/ByEmail/{row.Email}");
        }

        public string UpdateOneMain(AppUserModel row)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Name), "Name");
            content.Add(new StringContent(row.Email), "Email");
            content.Add(new StringContent(row.PhoneNumber), "PhoneNo");
            content.Add(new StringContent(row.RestaurantId), "RestaurantId");
            content.Add(new StringContent(row.RestaurantName), "RestaurantName");
            if (row.IsActive == true)
                content.Add(new StringContent("1"), "Status");
            else content.Add(new StringContent("0"), "Status");

            var result = base.UpdateOne($"{BaseUrl}/UpdatedOne", content);
            return result;
        }

        public string UpdateOneAdress(AppUserModel row)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Email), "Email");
            content.Add(new StringContent(row.Street), "Street");
            content.Add(new StringContent(row.City), "City");
            content.Add(new StringContent(row.State), "State");
            content.Add(new StringContent(row.Country), "Country");
            content.Add(new StringContent(row.PostalCode), "PostalCode");
            return base.UpdateOne($"{BaseUrl}/UpdatedOne", content);
        }
    }
}
