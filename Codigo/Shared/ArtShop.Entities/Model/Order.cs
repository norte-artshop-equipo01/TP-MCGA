//====================================================================================================
// Código base generado con Visual Studio: (Build 1.0.1973)
// Layered Architecture Solution Guidance
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

    public partial class Order : IdentityBase
    {
        public Order()
        {
            this.Disabled = false;
        }
        public DateTime OrderDate { get; set; }

        public double TotalPrice { get; set; }

        public int OrderNumber { get; set; }

        public int ItemCount { get; set; }

        public int ShippingId { get; set; }

        public string Email { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual Shipping Shipping { get; set; }
    }
}
