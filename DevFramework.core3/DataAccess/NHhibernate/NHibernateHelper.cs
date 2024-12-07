using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.DataAccess.NHhibernate
{
    //TContext le aynı işlemi yapar NHibernateHelper.
    //Farklı veri tabanlarının destekleniyor olması itibaren veri tabanına yönelik bir Helper yazmamız lazım.Biz Helpere desteklemek yerine
    //NHibernateHelper implementasyonu verip  oracel,sql,ADONET,deper gibi farklı veri tabanlarını configasyonunu yapıp istedigimiz veri
    //tabanı(sqp,orecal,Adonet vs)  kullanabilmemizi sağlıyoruz.
    //NHibernateHelperi bu projeye eklemek için NuGet paketinden Nhbirnate yüklemeniz lazım.
    public abstract class NHibernateHelper : IDisposable
    {
       
        
        private static ISessionFactory _sessionFactory;

        //NHibernate ile veritabanına bağlanma işlemlerini yönetmek için SessionFactory sınıfını kullanıyoruz.
        //SessionFactory sınfı yardımı ile NHibernate belirtilen veritabanına bağlanma işlemleri yönetiliriz.

        //protected(public ve private birleşimi) ortama göre değişkenlik gösterecektir.yani sql, oracel yada başka veri tabanında calışacaksan
        //farklılık gösterip ona göre initializefactory uygulayacaktır.

        //Protected; bir anlamda, public ve private erişim belirleyicilerinin birleşimi olarak görülebilmektedir.

        //Internal olarak tanımlanan bir değer; aynı program içerisinden erişilebilir, fakat farklı bir program
        //içerisinden erişilemez durumdadır. Program içerisinde herhangi bir kısıtlaması yoktur.
        public virtual ISessionFactory SessionFactory
        {
            //_sessionFactory döndür yada _sessionFactory Null ise InitializeFactory döndür.
            get { return _sessionFactory ?? (_sessionFactory = InitializeFactory()); }
        }
        //InitializeFactory bizim(oracel,sql vs) implementasyonu yani gönderdigim _sessionFactory(orecal,sql vs) kullanarak döndürür.
        //Bunuda InitializeFactory yapacak.
        //kişi nasıl bir ISessionFactory gönderdiyse onuda oluşturacak implemente(abstract ISessionFactory InitializeFactory();)
        //eden  InitializeFactory veriyor  ve onu kullanarak bana bir tane session açıyor.
        protected abstract ISessionFactory InitializeFactory();

        //contexti yani bu sessionı acmak için OpenSession metodu yapıyoruz.
        
        public virtual ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
