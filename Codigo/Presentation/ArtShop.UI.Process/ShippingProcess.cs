using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.UI.Process
{
    public class ShippingProcess: ProcessComponent
    {
        public List<Shipping> Listar()
        {
            var response = HttpGet<List<Shipping>>("/api/Shipping/Listar", new Dictionary<string, object>(), MediaType.Json);
            return response;
        }

        public Shipping Editar(Shipping ship)
        {
            var response = HttpPut<Shipping>("/api/Shipping/Editar", ship, MediaType.Json);
            return response;
        }

        public Shipping Agregar(Shipping ship)
        {
            var response = HttpPost<Shipping>("/api/Shipping/Agregar", ship, MediaType.Json);
            return response;
        }

        public Shipping GetById(int id)
        {
            var response = HttpGet<Shipping>("/api/Shipping/GetById", id, MediaType.Json);
            return response;

        }

        public void Eliminar(Shipping ship)
        {
            var response = HttpDelete<Shipping>("/api/Shipping/Eliminar", ship.Id, MediaType.Json);

        }

        public Shipping GetByCookie(string cookie)
        {
            var response = HttpGet<Shipping>("/api/Shipping/GetByCookie", cookie, MediaType.Json);
            return response;

        }
    }
}
