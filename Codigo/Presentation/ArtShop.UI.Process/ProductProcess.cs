using ArtShop.Business;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.UI.Process
{
     public class ProductProcess :ProcessComponent
    {
        private ProductBusiness biz = new ProductBusiness();
        public List<Product> ListarTodosLosProductos()
        {
            //return biz.ListarTodosLosArtistas();
            var response = HttpGet<List<Product>>("/api/Product/listar", new Dictionary<string, object>(), MediaType.Json);
            return response;
        }

        public Product EditarProduct(Product product)
        {
            //return biz.EditarArtista(artist);
            var response = HttpPut<Product>("/api/Product/Editar", product, MediaType.Json);
            return response;
        }

        public Product AgregarProducto(Product product)
        {
            //return biz.nuevo(artist);
            var response = HttpPost<Product>("/api/Product/Agregar", product, MediaType.Json);
            return response;
        }

        public Product GetById(int id)
        {
            var response = HttpGet<Product>("/api/Product/GetById", id, MediaType.Json);
            return response;
           // return biz.GetbyID(id);
        }

        public void EliminarProduct(Product product)
        {
            var response = HttpDelete<Product>("/api/Product/Eliminar", product.Id, MediaType.Json);
            //return response;
            // biz.Borrar(artist);
        }

    }
}
