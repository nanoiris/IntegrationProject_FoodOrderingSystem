using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class OrderListDto
    {
        public string RestaurantId { get; set; }
        public string id { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }
        public decimal PayTotal { get; set; }

        public bool IsDelivery { get; set; }
        public DateTime CreateTime { get; set; }
        // public DateTime PayTime { get; set; }

        public string StatusString
        {
            get
            {
                switch (Status)
                {
                    case (int)RmsStatusEnum.Paid:
                        return "Paid";
                    case (int)RmsStatusEnum.Accepted:
                        return "Accepted";
                    case (int)RmsStatusEnum.Ready:
                        return "Ready";
                    case (int)RmsStatusEnum.Completed:
                        return "Completed";
                    case (int)RmsStatusEnum.Canceled:
                        return "Canceled";
                    default:
                        return "Unknown";
                }
            }
        }

    }

}