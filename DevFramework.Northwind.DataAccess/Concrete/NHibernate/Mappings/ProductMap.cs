using DevFramework.Northwind.Entities.Concrete;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Mappings
{
   public class ProductMap:ClassMap<Product>
    {
        //ctor vasıtasıyla bu nesnenin hangi tabloya gideceginiz belirtiyoruz.
        public ProductMap()
        {
            Table(@"Products");
            //LazyLoad Kullanacağımız nesneleri, nesnenin ihtiyaç anından çok önce yaratır ve bekletir. 
            LazyLoad();

            Id(x => x.ProductId).Column("ProductID");
            Map(x => x.CategoryId).Column("CategoryID");
            Map(x => x.ProductName).Column("ProductName");
            Map(x => x.QuantityPerUnit).Column("QuantityPerUnit");
            Map(x => x.UnitPrice).Column("UnitPrice");

        }
    }
}
