﻿using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.UI.Process
{
    public class CartItemProcess : ProcessComponent
    {
        public List<CartItem> ListarCartItem()
        {
            var response = HttpGet<List<CartItem>>("/api/CartItem/Listar", new Dictionary<string, object>(), MediaType.Json);
            return response;
        }

        public CartItem EditarCartItem(CartItem cartitem)
        {
            var response = HttpPut<CartItem>("/api/CartItem/Editar", cartitem, MediaType.Json);
            return response;
        }

        public CartItem AgregarCartItem(CartItem cartitem)
        {
            var response = HttpPost<CartItem>("/api/CartItem/Agregar", cartitem, MediaType.Json);
            return response;
        }

        public CartItem GetById(int id)
        {
            var response = HttpGet<CartItem>("/api/CartItem/GetById", id, MediaType.Json);
            return response;

        }
        public List<CartItem> GetByCartId(int id)
        {
            var response = HttpGet<List<CartItem>>("/api/CartItem/GetByCartId", id, MediaType.Json);
            return response;

        }

        public void EliminarCartItem(int id)
        {
            var response = HttpDelete<CartItem>("/api/CartItem/Eliminar", id, MediaType.Json);

        }

       
    }
}
