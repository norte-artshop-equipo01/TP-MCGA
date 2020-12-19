using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ArtShop.Entities.Model
{
    /// <summary>
    /// Order Metadata class
    /// </summary>
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
        public class OrderMetadata
        {
            /// <summary>
            /// Id
            /// </summary>        
            [DisplayName("Id")]
            [Required(ErrorMessage = "Requerido")]
            public int
              Id
            {
                get;
                set;
            }

            /// <summary>
            /// User Id
            /// </summary>        
            [DisplayName("ShippingId")]
            [Required(ErrorMessage = "Requerido")]
            
            public int ShippingId { get; set; }
            

            [DisplayName("Email")]
            [Required(ErrorMessage = "Requerido")]
            [MaxLength(100, ErrorMessage = "Cookie Longitud  100 caracteres")]
            public string
             Email
            {
                get;
                set;
            }


            /// <summary>
            /// Order Date
            /// </summary>        
            [DisplayName("Order Date")]
            [Required(ErrorMessage = "Requerido")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime OrderDate { get; set; }

            /// <summary>
            /// Total Price
            /// </summary>        
            [DisplayName("Total Price")]
            [Required(ErrorMessage = "Requerido")]
            public double
              TotalPrice
            {
                get;
                set;
            }

            /// <summary>
            /// Order Number
            /// </summary>        
            [DisplayName("Order Number")]
            [Required(ErrorMessage = "Requerido")]
            public int
              OrderNumber
            {
                get;
                set;
            }

            /// <summary>
            /// Item Count
            /// </summary>        
            [DisplayName("Item Count")]
            [Required(ErrorMessage = "Requerido")]
            public int
              ItemCount
            {
                get;
                set;
            }



        }
    }
}
