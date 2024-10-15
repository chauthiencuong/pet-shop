using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChauThienCuong_PetShop.Context;


namespace ChauThienCuong_PetShop.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}