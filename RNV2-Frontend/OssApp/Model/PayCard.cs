using RestaurantDaoBase.Enums;

namespace OssApp.Model
{
    public class PayCard
    {
        public PayTypeEnum? CardType { get; set; } 
        public string? CardName { get; set; }
        public string? CardNo { get; set; }
        public string? Cvv { get; set; }
        public DateTime? ValidTime { get; set; }
    }
}
