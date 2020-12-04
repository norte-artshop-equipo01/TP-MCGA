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
        public List<OrderDetail> ListarTodas()
        {
            return db.Get();
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
    }
}
