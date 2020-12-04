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
        private BaseDataService<OrderDetail> db2 = new BaseDataService<OrderDetail>();
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
            
            for (int i = 0; i < order.OrderDetail.Count(); i++)
            {
                
                db2.Delete(order.OrderDetail.ElementAt(i));
                
            }

            db.Delete(order.Id);
        }
    }
}
