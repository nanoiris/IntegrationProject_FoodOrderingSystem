using OssApp.Model;
using System.Net.Http.Json;
using Serilog;
using RestaurantDaoBase.Models;

namespace OssApp.Services
{
    public class RoleService : RestService<RoleModel>
    {
        public static readonly string BaseUrl = "api/Roles";
        
        public RoleService(string identityServer) : base(identityServer) { }
        
        public async Task<List<RoleModel>> List()
        {
            var result = await this.List(BaseUrl);
            return result;
        }

        public string AddNewOne(RoleModel role)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(role.Name), "Name");
            return base.AddNewOne(BaseUrl,multipartContent);
        }

        public bool DeleteOne(RoleModel role)
        {
            return base.DeleteOne($"{BaseUrl}/{role.Id}");
        }

        public bool UpdateOne(RoleModel role)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(role.Name), "Name");
            multipartContent.Add(new StringContent(role.Id), "Id");

            return base.UpdateOne(BaseUrl, multipartContent);
        }
    }
}
