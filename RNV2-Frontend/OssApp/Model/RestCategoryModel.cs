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

        public RestCategoryModel() { }
        public RestCategoryModel(RestCategoryModel row)
        {
            Id = row.Id;
            Name = row.Name;
            Logo = row.Logo;
            UploadFile = row.UploadFile;
            FileSize = row.FileSize;
            FileName = row.FileName;
        }
        public void CopyFrom(RestCategoryModel row)
        {
            Name = row.Name;
            Logo = row.Logo;
            UploadFile = row.UploadFile;
            FileSize = row.FileSize;
            FileName = row.FileName;
        }
    }
}
