using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChauThienCuong_PetShop.Context;

namespace ChauThienCuong_PetShop.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }

        public List<Category> ListCategory { get; set; }
    }
}