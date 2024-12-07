using DevFramework.core3.DataAccess;
using DevFramework.core3.DataAccess.EntityFramework;
using DevFramework.core3.DataAccess.NHhibernate;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.Concrete.Managers;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
    //BussinesModule adında bir class oluşturup ve NinjectModule üzerinden miras alma işlemini gerçekleştiriyoruz. apide
    //,bussines de ,arayüz katmanında ,servis katmanında kullanıyor olacagız
    //Dependency injectiona ihtiyacımız var yani bizim hiç bir zaman ne managera nede veri erişim katmanına bagımlı olmamamız gerekiyor.
    //Bu bakımdan bussines katmanında ben Dependency injectionla iligi konfigirasyonu yapmak istiyorum yani ben manager katmanında EF veya Nh le
    //çalışmak istiyorum veya ben arayüz katmanında business la direk DN üzerinde çalışmak istiyorum  yada Api üzerinden çalışmak istiyorum.
    //veya ben WCf servisi üzerinden calısmak istiyorum.Bunun için dependecy injection contair a ihtiyacımız var.
    public class BussinesModule:NinjectModule
    {
        //Ardından “DependencyResolver” (Bağımlılık Çözücü) adında bir klasör oluşturuyorum ve altına “BusinessModule” class’ı oluşturuyorum.
        //BusinessModule” adlı class’ımızı “NinjectModule” abstract class’ından implemente ederek Load methodunu override ediyoruz.
        //Load methodumuz içerisinde “IProductService” ile “ProductManager” ‘ı “IProductDal” ile de “EfProductDal” ‘ı birbirine
        //bağlıyoruz. Yani “IProductService” talebinde bulunulursa “ProductManager” instance(object yani Nesne)’nı oluştur
        //eğer “IProductDal” talebinde bulunulursa ise “EfProductDal” instance(object yani Nesne)’nı oluştur diyoruz.


        //InSingletonScope() methodu ile de bu nesnelerin tek bir defa üretilmesini sağlıyoruz.
        //Bu yapılandırmaları Business (İş) Katmanı’nda yaparak diğer UI (User Interface) katmanlarında, Web API projelerimizde kullanabiliriz.

        //Ninject’in run-time sırasında bize birer instance oluşturmasını istiyoruz, bunun için de bizim hangi abstract (interface)
        //karşısına hangi concrete (örnek nesne) geleceğini belirtmemiz lazım Ninject’e. Bunun için bir class oluşturup NinjectModule
        //üzerinden miras almamız gerekiyor. 

        //Miras işleminden sonra bizden Load() metotunu override etmemizi ve gerekli tanımlamaları yapmamızı istiyor..

        //Dependency injection kaba tabir ile bir sınıfın/nesnenin bağımlılıklardan kurtulmasını amaçlayan ve o nesneyi
        //olabildiğince bağımsızlaştıran bir programlama tekniği/prensibidir.
        public override void Load()
        {
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            //EfProductDal la calıştıgımı söylüyorum yarın öbür gün NhProductDal calışacaksam Bind<IProductDal>().To<NhProductDal>();yazmam yeterli.
            Bind<IProductDal>().To<EfProductDal>().InSingletonScope();
            Bind<IUserService>().To<UserManager>();
            Bind<IUserDal>().To<EfUserDal>();
            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQureyableRepository<>));
            //Eğer senden DbContext türünden  bir nesne isterse o zaman onu NorthwindContext bağla.
            Bind<DbContext>().To<NorthwindContext>();
            //Eğer senden NHibernateHelper implamentasyonuna ihtiyac duyarsa beni SqlServerHelper bağla.
            Bind<NHibernateHelper>().To<SqlServerHelper>();

        }
       
    }
}
