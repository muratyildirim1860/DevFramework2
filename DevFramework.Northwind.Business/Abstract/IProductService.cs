using DevFramework.Northwind.Entities.Concrete;
using System.Collections.Generic;

namespace DevFramework.Northwind.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();

        Product GetById(int İd);

        Product Add(Product product);

        Product Update(Product product);
        //Birden fazla operasyonu yapmamız durumunda TransactionalOperation metdounu uyguluyorum.
        //örnek olarak biz metodun içerisinden veri tabanına bir kayıt ekliyoruz hemen arkasından başka bir kaydı günceliyoruz 
        //ama bunu aynı metodun içerisinden yapabilecegimiz iş sınıfları olabilir fakat arka arkaya 3 işlem yaptıgımızda 1 işlem başarılı
        //olabilir 2 inci işlem başarılı olabilir ama 3üncü işlem başarısız olabilir ozaman bu durumda bizim karar vermemiz lazım
        //önceki işlemleri geri almamızmı gerekecek yoksa bu şekildemi devam edecegiz.
        void TransactionalOperation(Product product1,Product product2);
     

        //TransactionalOperation:

        //İşlemi yapmayı veya geri almayı unutmanız, sorunlara neden olabilir ve veri kaybı veya tutarsız veri sorunları olabilir.
        //Bu tür sorunlar TransactionScope kullanılarak çözülebilir.
        //TransactionScope'u kullanma sürecinde, C# geliştiricileri için işlemleri kaydetmek (taahhüt etmek) veya işlemleri geri almak için manuel bir seçenek yoktur.
        //Karşılaşılan bir istisna varsa, işlemler otomatik olarak geri alınacak ve istisna, yakalama bloğunda yakalanacaktır.
        //İşlemi tamamladığınızda, TransactionScope'u taahhüt edebilirsiniz. Taahhüt ettiğiniz anda, eksiksiz çağrılacaktır. Bu nedenle, elektrik kesintisi, sistem çökmesi
        //veya donanım arızası durumunda TransactionScope, işlemde bir istisna olduğunu kabul eder ve TransactionScope bloğu içindeki tüm işlemler otomatik olarak geri
        //alınır.TransactionScope, birden çok veritabanının yanı sıra birden çok bağlantı dizesine sahip tek bir veritabanını korumak için kullanılabilir.
        //TransactionScope ile çalışırken, çalışma arasında herhangi bir veritabanı bağlantısını kapatmanız gerekmez.



    }
}
