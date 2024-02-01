﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.DTO.Brand
{
    public class UpdateBrandDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }
        [Required]
        public int EstablishYear { get; set; }
    }
}
