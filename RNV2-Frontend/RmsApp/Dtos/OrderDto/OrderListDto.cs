using System;
using System.ComponentModel.DataAnnotations;
namespace RmsApp.Dtos
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public bool IsDelivery { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? DesiredTime { get; set; }

        public OrderStatusEnum Status { get; set; }

    }
}