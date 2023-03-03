using Microsoft.Maui.Controls;
using OssApp.Model;
using Radzen.Blazor;
using Serilog;
using System.Net.Http.Json;

namespace OssApp.Services
{
    public class RestCategoryService : RestService<RestCategoryModel>
    {
        public static readonly string BaseUrl = "api/RestCategory";
        public string LogoUploadUrl { get; set; } 
        public RestCategoryService(string server) : base(server) 
        {
            this.LogoUploadUrl = $"{server}/api/Logo/RestCategory/";
        }
       
        public async Task<List<RestCategoryModel>> List()
        {
            var result = await this.List($"{BaseUrl}/List");
            result.ForEach(row => {
                if(row.Logo!= null) 
                  row.Logo = Utils.BuildLogoPath(row.Logo);
            });
            return result;
        }

        public string AddNewOne(RestCategoryModel row)
        {

            if(string.IsNullOrEmpty(row.Name))
                return null;

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Name), "Name");
            return base.AddNewOne($"{BaseUrl}/NewOne", content);
        }
       public bool DeleteOne(RestCategoryModel row)
       {
          return base.DeleteOne($"{BaseUrl}/DeletedOne/{row.Id}");
       }
        public string UpdateOne(RestCategoryModel row)
        {
            if (string.IsNullOrEmpty(row.Name))
                return null;

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(row.Id), "Id");
            content.Add(new StringContent(row.Name), "Name");

            return base.UpdateOne($"{BaseUrl}/UpdatedOne", content);
        }
    }
}
