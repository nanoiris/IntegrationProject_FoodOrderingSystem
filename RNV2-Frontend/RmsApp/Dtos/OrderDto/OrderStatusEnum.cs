using System;
using System.ComponentModel.DataAnnotations;
using RmsApp.Dtos;

namespace RmsApp.Dtos
{
    public enum OrderStatusEnum
    {
        Paid = 1, Accepted = 2, Ready = 3, Completed = 4, Canceled = 5
    }
}