using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business
{
    class Class1
    {
        //Business (İş) Katmanı
        //1-Business katmanımızda da soyut nesnelerimiz için “Abstract” klasörü soyut nesnelerimizden implemente edeceğimiz
        //somut nesnelerimiz için ise “Concrete” klasörü oluşturuyoruz.
        //2-Abstract ve Concrete klasörlerini oluşturduktan sonra Abstract klasörü altına “IProductService” interface’ini oluşturuyorum.
        //3-Product listesi döndürecek bir method tanımlıyorum burada Product nesnesini kullanabilmek için
        //“DevFramework.Northtwind.Entities” projemizi referans eklemek durumundayız.
        //4-Ardından Concrete klasörü altına “ProductManager” adında bir class oluşturuyorum.


        //İş katmanı benim projemi ilgilendiren iş şüreçlerini burda yapıyoruz.
        //Örnegin bir kişinin eğliyet alma ihtiyacı varsa bu kişiye eğliyet verelimmi diye kontrollerini yaparız yani ilk yardımdan 70 almişmı
        //,motordan 60 almışmı vs 
        //Ara yüz ve veri tabanı arasındaki iletişimi bu katmanda uyguluyoruz.
    }
}
