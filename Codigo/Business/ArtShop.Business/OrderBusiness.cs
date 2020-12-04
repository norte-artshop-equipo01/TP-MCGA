using ArtShop.Data;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Business
{
    public class OrderBusiness
    {
        private BaseDataService<Order> db = new BaseDataService<Order>();
        public List<Order> ListarTodas()
        {
            return db.Get();
        }

        public Order EditarOrder(Order order)
        {
            return db.Update(order, order.Id);
        }

        public Order nuevo(Order order)
        {
            order = db.Create(order);

            return (order);
        }
        public Order GetbyID(int id)
        {
            return db.GetById(id);
        }
        public void Borrar(Order order)

        {
            //BaseDataService<Product> db2 = new BaseDataService<Product>();
            //Product product = new Product();
            //for (int i = 0; i < artist.Product.Count; i++)
            //{
            //    product = artist.Product.ElementAt(i);
            //    db2.Delete(product);
            //}

            db.Delete(order.Id);
        }
    }
}
