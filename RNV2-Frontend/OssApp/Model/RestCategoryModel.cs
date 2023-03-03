namespace OssApp.Model
{
    public class RestCategoryModel : LogoModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public override string? Logo { get; set; }
        public string? UploadFile { get; set; }
        public long? FileSize { get; set; }
        public string? FileName { get; set; }
    }
}
