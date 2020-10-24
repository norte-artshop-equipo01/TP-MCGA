//====================================================================================================
// Código base generado con Visual Studio: (Build 1.0.1973)
// Layered Architecture Solution Guidance
// Generado por vcontreras - MCGA
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;


namespace ArtShop.Entities.Model
{
    
 [Serializable]
    public partial class Artist : IdentityBase
    {

        public Artist()
        {
            this.Product = new HashSet<Product>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LifeSpan { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int TotalProducts { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        public bool Disabled { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}