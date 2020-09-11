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
    public partial class Cart : IdentityBase
    {
        public Cart()
        {
            this.CartItem = new HashSet<CartItem>();
        }

        public string Cookie { get; set; }

        public DateTime CartDate { get; set; }

        public int ItemCount { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }


    }
}
