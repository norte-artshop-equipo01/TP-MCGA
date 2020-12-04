using ArtShop.Data;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Business
{
    public class OrderDetailBusiness
    {
        
        private BaseDataService<OrderDetail> db = new BaseDataService<OrderDetail>();
        private BaseDataService<Product> db2 = new BaseDataService<Product>();
        public List<OrderDetail> ListarTodas()
        {
            var orderdetaillist = db.Get();
            foreach (OrderDetail orderdetail in orderdetaillist)
            {
                orderdetail.Product = db2.GetById(orderdetail.ProductId);
                
                /*order.ItemCount = order.OrderDetail.Count;*/
            }
            return orderdetaillist;
        }

        public OrderDetail EditarOrderDetail(OrderDetail orderdetail)
        {
            return db.Update(orderdetail, orderdetail.Id);
        }

        public OrderDetail nuevo(OrderDetail orderdetail)
        {
            orderdetail = db.Create(orderdetail);

            return (orderdetail);
        }
        public OrderDetail GetbyID(int id)
        {
            return db.GetById(id);
        }
        public void Borrar(OrderDetail orderdetail)

        {
            db.Delete(orderdetail.Id);
        }

        public List<OrderDetail> GetbyOrderId(int id)
        {
            return db.Get(x => x.OrderId == id).ToList();
        }
    }
}
