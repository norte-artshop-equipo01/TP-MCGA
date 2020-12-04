using ArtShop.Business;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.UI.Process
{
    public class CartProcess : ProcessComponent
    {
        public List<Cart> ListarCart()
        {
            var response = HttpGet<List<Cart>>("/api/Cart/Listar", new Dictionary<string, object>(), MediaType.Json);
            return response;
        }

        public Cart EditarCart(Cart cart)
        {
            var response = HttpPut<Cart>("/api/Cart/Editar", cart, MediaType.Json);
            return response;
        }

        public Cart AgregarCart(Cart cart)
        {
            var response = HttpPost<Cart>("/api/Cart/Agregar", cart, MediaType.Json);
            return response;
        }

        public Cart GetById(int id)
        {
            var response = HttpGet<Cart>("/api/Cart/GetById", id, MediaType.Json);
            return response;
            
        }

        public void EliminarCart(Cart cart)
        {
            var response = HttpDelete<Cart>("/api/Cart/Eliminar", cart.Id, MediaType.Json);
           
        }

        public Cart GetByCookie(string cookie)
        {
            var response = HttpGet<Cart>("/api/Cart/GetByCookie", cookie, MediaType.Json);
            return response;

        }

    }
}
