using RestaurantDaoBase.Enums;

namespace OssApp.Model
{
    public class OrderModel
    {
        public string? Id { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public StatusEnum? Status { get; set; }
        public DateTime? DesiredTime { get; set; }
        public string? UserName { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? PayTime { get; set; }
        public PayCard? Card { get; set; }

        public decimal? PayTotal { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Tips { get; set; }

        public bool? IsDelivery { get; set; }
        public List<OrderItem>? ItemList { get; set; }

        public string StrPayTotal => PayTotal == null ? "0.00" : string.Format("0.00", PayTotal);
        public string StrSubTotal => SubTotal == null ? "0.00" : string.Format("0.00", SubTotal);
        public string StrDeliveryFee => DeliveryFee == null ? "0.00" : string.Format("0.00", DeliveryFee);
        public string StrTax => Tax == null ? "0.00" : string.Format("0.00", Tax);
    }
}
