using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SparkArtApp.Models
{
    public class ModelBase
    {
        [JsonProperty("<Id>k__BackingField")]
        public int Id { get; set; }

        [JsonProperty("<CreatedOn>k__BackingField")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedOn { get; set; }

        [JsonProperty("<CreatedBy>k__BackingField")]
        public string CreatedBy { get; set; }

        [JsonProperty("<ChangedOn>k__BackingField")]
        //[MaxLength(256, ErrorMessage = "Created By Longitud  256 caracteres")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ChangedOn { get; set; }

        [JsonProperty("<ChangedBy>k__BackingField")]
        public string ChangedBy { get; set; }


    }
}
