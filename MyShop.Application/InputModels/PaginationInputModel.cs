﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.InputModels
{
    public class PaginationInputModel
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
