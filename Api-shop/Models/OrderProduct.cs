﻿using System.ComponentModel.DataAnnotations;

namespace WebApiUseSqlLiteItog.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }
    }

}