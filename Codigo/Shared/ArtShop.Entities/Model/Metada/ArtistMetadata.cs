using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtShop.Entities.Model
{
    [MetadataType(typeof(ArtistMetadata))]
    public partial class Artist
    {
        public class ArtistMetadata
        {
            [DisplayName("First Name")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(50, ErrorMessage = "Longitud  50 caracteres")]
            public string FirstName { get; set; }
                    
            [DisplayName("Last Name")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(50, ErrorMessage = "Longitud  50 caracteres")]
            public string LastName { get; set; }

            [DisplayName("Life Span")]
            [MaxLength(10, ErrorMessage = "Longitud  10 caracteres")]
            public string LifeSpan { get; set; }

            [DisplayName("Country")]
            [MaxLength(50, ErrorMessage = "Longitud  50 caracteres")]
            public string Country { get; set; }

            [DisplayName("Description")]
            [MaxLength(2000, ErrorMessage = "Longitud  2000 caracteres")]
            public string Description { get; set; }

            [DisplayName("Total Products")]
            [Required(ErrorMessage = "Requerido")]
            public int TotalProducts { get; set; }

            [NotMapped]
            [DisplayName("Full Name")]
            public string FullName { get { return FirstName + " " + LastName; } }

        }
    }
}
