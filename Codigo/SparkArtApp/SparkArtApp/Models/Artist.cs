using SparkArtApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparkArtApp.Models
{
    public partial class Artist : ModelBase
    {
        [JsonProperty("<FirstName>k__BackingField")]
        public string FirstName { get; set; }

        [JsonProperty("<LastName>k__BackingField")]
        public string LastName { get; set; }

        [JsonProperty("<LifeSpan>k__BackingField")]
        public string LifeSpan { get; set; }

        [JsonProperty("<Country>k__BackingField")]
        public string Country { get; set; }

        [JsonProperty("<Description>k__BackingField")]
        public string Description { get; set; }

        [JsonProperty("<TotalProducts>k__BackingField")]
        public int TotalProducts { get; set; }

        public string FullName { get { return $"{ FirstName } { LastName }"; } }
        public bool Disabled { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
