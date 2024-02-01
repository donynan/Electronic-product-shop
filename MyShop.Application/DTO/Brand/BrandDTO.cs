using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.DTO.Brand
{
    public class BrandDTO
    {
   
        public int Id { get; set; }

  
        public String Name { get; set; }

        public int EstablishYear { get; set; }
    }
}
