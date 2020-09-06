//====================================================================================================
// Código base generado con Visual Studio: (Build 1.0.1973)
// Unidad 4 - Layered Architecture
// Generado por vcontreras - MCGA
//====================================================================================================


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;


namespace ArtShop.Entities.Model
{
    [Serializable]
    public partial class Product : IdentityBase
    {
        public Product()
        {
            this.CartItem = new HashSet<CartItem>();
            this.OrderDetail = new HashSet<OrderDetail>();
            this.Rating = new HashSet<Rating>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int QuantitySold { get; set; }

        public double AvgStars { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }


    }
}
