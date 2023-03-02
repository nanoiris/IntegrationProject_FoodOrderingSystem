using Microsoft.AspNetCore.Components.Forms;
using RestaurantDaoBase.Models;

namespace OssApp.Model
{
    public class RestCategoryModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? UploadFile { get; set; }
        public long? FileSize { get; set; }
        public string? FileName { get; set; }
    }
}
