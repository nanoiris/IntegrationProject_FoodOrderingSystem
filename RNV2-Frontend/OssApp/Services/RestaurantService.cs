﻿using OssApp.Model;

namespace OssApp.Services
{
    public class RestaurantService : RestService<RestaurantForm>
    {
        public static readonly string BaseUrl = "api/Restaurant";
        
        public Dictionary<string,string> CategoryDict { get; set; } 
        public string LogoUploadUrl { get; set; } 
        public RestaurantService(string server) : base(server) 
        {
            this.LogoUploadUrl = $"{server}/api/Logo/Restaurant/";
        }

        public void BuildCateDict(IEnumerable<RestCategoryModel> categories)
        {
            this.CategoryDict = new Dictionary<string,string>();
            foreach (RestCategoryModel category in categories) 
            {
                CategoryDict.Add(category.Id, category.Name);
            }
        }

        public async Task<List<RestaurantForm>> List()
        {
            var result = await this.List($"{BaseUrl}/List");
            result.ForEach(row => {
                if(row.Logo!= null) 
                  row.Logo = Utils.BuildLogoPath(row.Logo);
                row.CategoryName = CategoryDict[row.CategoryId];
            });
            return result;
        }

        public string AddNewOne(RestaurantForm row)
        {

            if(string.IsNullOrEmpty(row.Name))
                return null;

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Name), "Name");
            content.Add(new StringContent(row.Description), "Description");
            content.Add(new StringContent(row.IsFeatured.ToString()), "IsFeatured");
            content.Add(new StringContent(row.CategoryId), "CategoryId");

            return base.AddNewOne($"{BaseUrl}/NewOne", content);
        }
       public bool DeleteOne(RestaurantForm row)
       {
          return base.DeleteOne($"{BaseUrl}/DeletedOne/{row.Id}");
       }

        public string UpdateOneMain(RestaurantForm row)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Id), "Id");
            content.Add(new StringContent(row.Name), "Name");
            content.Add(new StringContent(row.Description), "Description");
            content.Add(new StringContent(row.IsFeatured.ToString()), "IsFeatured");
            content.Add(new StringContent(row.CategoryId), "CategoryId");
            var result = base.UpdateOne($"{BaseUrl}/UpdatedOne", content);
            row.CategoryId = result;
            row.CategoryName = CategoryDict[row.CategoryId];
            return result;
        }

        public string UpdateOneAdress(RestaurantForm row)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Id), "Id");
            content.Add(new StringContent(row.Street), "Street");
            content.Add(new StringContent(row.City), "City");
            content.Add(new StringContent(row.State), "State");
            content.Add(new StringContent(row.Country), "Country");
            content.Add(new StringContent(row.PostalCode), "PostalCode");
            content.Add(new StringContent(row.Email), "Email");
            content.Add(new StringContent(row.PhoneNo), "PhoneNo");
            return base.UpdateOne($"{BaseUrl}/UpdatedOne", content);
        }
    }
}
