using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparkArtApp.Models
{
    public partial class Product : ModelBase
    {
        public Product()
        {
            //this.CartItem = new HashSet<CartItem>();
            //this.OrderDetail = new HashSet<OrderDetail>();
            //this.Rating = new HashSet<Rating>();
        }

        [JsonProperty("<Title>k__BackingField")]
        public string Title { get; set; }

        [JsonProperty("<Description>k__BackingField")]
        public string Description { get; set; }
        
        [JsonProperty("<Image>k__BackingField")]
        public string Image { get; set; }

        [JsonProperty("<Price>k__BackingField")]
        public double Price { get; set; }

        [JsonProperty("<QuantitySold>k__BackingField")]
        public int QuantitySold { get; set; }

        [JsonProperty("<Disabled>k__BackingField")]
        public bool Disabled { get; set; }

        [JsonProperty("<AvgStars>k__BackingField")]
        public double AvgStars { get; set; }

        [JsonProperty("<ArtistId>k__BackingField")]
        public int ArtistId { get; set; }

        [JsonProperty("<Artist>k__BackingField")]
        public virtual Artist Artist { get; set; }
        //public virtual ICollection<CartItem> CartItem { get; set; }
        //public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        //public virtual ICollection<Rating> Rating { get; set; }
    }
}