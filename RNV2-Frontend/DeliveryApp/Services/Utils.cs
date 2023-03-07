namespace DeliveryApp.Services
{
    public class Utils
    {
        public static async Task<string> BuildLogoPath(string logoPath)
        {
            return $"/img/delivery/{logoPath}";
        }
    }
}
