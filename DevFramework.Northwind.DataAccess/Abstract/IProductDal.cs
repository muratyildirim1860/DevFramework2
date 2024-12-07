using DevFramework.core3.DataAccess;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Abstract
{//Veri erişim katmanı
  public  interface IProductDal:IEntityRepository<Product>
    {
        //2-İlgili nesne için IProdcutDal oluştururuz.

        //Abstract klasörümüz altına bir “IProductDal” adında interface oluşturuyorum. “Dal” isimlendirmesi
        //Data Access Layer(Veri Erişim Katmanı)’dan gelmektedir. Interface’imize Product listesi döndüren bir method yazıyoruz.


        //IProdcutDal için IEntityRepository dan <Product> imlementasyonunu yapıyoruz.
        //Neden IProductDal yerine sadece IEntityRepository kullanmadık dersek.çünkü Product için farklı metotlar ve joinlerde yazabiliriz
        //bundan dolayı IProductDal şeklinde bir interface oluşturuyoruz.Aşagıdaki örnekte oldugu gibi  List<ProductDetail> GetProductDetails();


        //Complex Types klasöründe bulunan ProductDetails sınıfında ProductId,CategoryName,ProductName objeler tüm listelerini getiren
        //List<ProductDetail> GetProductDetails(); metodunu yazdık.
        List<ProductDetail> GetProductDetails();
    }
}
