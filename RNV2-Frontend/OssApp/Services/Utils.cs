namespace OssApp.Services
{
    public class Utils
    {
        public static string BuildLogoPath(string logoPath)
        {
            return $"https://pxtoday.blob.core.windows.net/pxtoday/{logoPath}";
        }
    }
}
