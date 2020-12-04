using ArtShop.Data;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Business
{
    public class ProductBusiness
    {
        private BaseDataService<Artist> db2 = new BaseDataService<Artist>();
        private BaseDataService<Product> db = new BaseDataService<Product>();
        public List<Product> ListarTodosProductos()
        {
            var product = db.Get();
            foreach (Product prod in product)
            {
                prod.Artist = db2.GetById(prod.ArtistId);
            }
            return product;
        }

        public Product EditarProduct(Product product)
        {
            return db.Update(product, product.Id);
        }

        public Product Nuevo(Product product)
        {
           product = db.Create(product);

            return (product);
        }
        public Product GetbyID(int id)
        {
            return db.GetById(id);
        }
        public void Borrar(Product product)
        {
            //for (int i = 0; i < artist.Product.Count; i++)
            //{
            //    _database.Remove(artist.Product.ElementAt(i));
            //}

            db.Delete(product.Id);
        }
    }
}
