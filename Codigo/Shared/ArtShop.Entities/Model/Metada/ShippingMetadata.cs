using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArtShop.Entities.Model
{
    
    [MetadataType(typeof(ShippingMetadata))]
    public partial class Shipping
    {
        public class ShippingMetadata
        {
            //[DisplayName("Id")]
            //[Required(ErrorMessage = "Requerido")]
            //public int Id
            //{
            //    get;
            //    set;
            //}

            [DisplayName("FirstName")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(30, ErrorMessage = "Cookie Longitud  30 caracteres")]
            public string
              FirstName
            {
                get;
                set;
            }

                    
            [DisplayName("LastName")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(30, ErrorMessage = "Cookie Longitud  30 caracteres")]
            public string
              LastName
            {
                get;
                set;
            }

            [DisplayName("Email")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(100, ErrorMessage = "Cookie Longitud  100 caracteres")]
            public string
              Email
            {
                get;
                set;
            }

            [DisplayName("Address")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(300, ErrorMessage = "Cookie Longitud  300 caracteres")]
            public string
             Address
            {
                get;
                set;
            }

            [DisplayName("City")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(30, ErrorMessage = "Cookie Longitud  30 caracteres")]
            public string
             City
            {
                get;
                set;
            }

            [DisplayName("Country")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(30, ErrorMessage = "Cookie Longitud  30 caracteres")]
            public string
             Country
            {
                get;
                set;
            }
                        
        }
    }
}
