using System;
using System.ComponentModel.DataAnnotations;
namespace RmsApp.Dtos
{
    public class CategoryAddDto
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }

    }
}