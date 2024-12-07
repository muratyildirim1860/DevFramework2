using DevFramework.core3.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    //NorthwindContext nesnesini Dependency Injection ile talep ederek ilgili controller de gerekli veritabanı işlemlerini gerçekleştiriyoruz.
    //böylece veritabanımız üzerinde işlemlerimizi aşagıdaki kodlarla gerçekleştirmiş bulunmaktayız.

    //5-EfProductDal adında bir sınıf oluşturuyoruz ve  EfEntityRepositoryBase implement(uygulanması) edip ve  Product nesnesiye çalışıp
    //ordan  NorthwindContext e göndermesiyle son olarak da  IProdcutDal dan implement ediyoruz.
    //Ürünün update,delete,add,getlist,get kodlarını uygularız.
    

    //Bu sınıfı “IProductDal” interface’nden implemente edeceğiz. Bu sınıfın içerisinde sadece Entity Framework kodlarını yazacağız
    //böylece SOLID Prensipleri’nin “S” harfi olan Single Responsibility (Tek Sorumluluk) Prensibi’ni de uygulamış oluyoruz çünkü
    //Single Responsibility Prensibi yazdığımız methodun veya sınıfın tek bir sorumluluğa ait olması gerektiğini söylemektedir.


    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        //EntityFremaworkda bir complex tipte çalısmak için aşagıdaki metodaları uyguluyoruz.
        //ProductId gelecek karşısında CategoryName ve ProductName gelecek şekilde listelenecekdir.
        public List<ProductDetail> GetProductDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                //Context deki productslarla 
                var result = from p in context.Products
                             //p.CategoryId ile c.CategoryId eşit olacak diyoruz.
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDetail
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName

                             };

                return result.ToList();
            }
            
        }
    }
}
