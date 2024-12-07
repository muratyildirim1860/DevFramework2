using DevFramework.core3.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.DataAccess.EntityFramework
{
    public class EfQureyableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        
        //DBContet veritabanımızla uygulamamız arasında sorgulama,
        //güncelleme, silme gibi işlemleri yapmamız için olanak sağlar. Yani veritabanı içinde yer alan verilerimizle alakalı olarak
        //her türlü süreçte iletişimimizi sağlayan bir classtır.

        //IDbSet olarak tanımladığın property(get;set;) ise  veritabanındaki Table'e karşılık gelen tabloyu temsil etmektedir.
        //yani  T (customer,product,employess)gönderdiğimizde  IDbSete abone olacak.

        private DbContext _dbContext;
        
        private IDbSet<T> _Entities;


        public EfQureyableRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Table bir tabloya abone olup ve hangi T(customer,product,employes) nesnesini veririlirse o tabloya abone olacak.
        //IQeryable Contecxt ti kapatmadan 1 den fazla sorguyu uygulamak için kullanırız.

        public IQueryable<T> Table => this.Entities;

        //yada public IQueryable<T> Table
        //{
        //get{return this.Entities;}
        //}
        //IDbSet olarak tanımladığın property(get;set;) ise  veritabanındaki Table'e karşılık gelen tabloyu temsil etmektedir.
        //yani  T (customer,product,employess)gönderdiğimizde  IDbSete abone olacak.

        //Virtual olarak tanımladığımız metodlarımızı, diğer class larda override edebiliriz. Yani, kalıtıldığı(miras alındığı)
        //sınıfta metodun gövdesini ( süslü parantezlerin içi) değiştirebileceğimiz anlamına gelir.

        //Protected; bir anlamda, public ve private erişim belirleyicilerinin birleşimi olarak görülebilmektedir. Internal olarak
        //tanımlanan bir değer; aynı program içerisinden erişilebilir, fakat farklı bir program içerisinden erişilemez durumdadır.
        //Program içerisinde herhangi bir kısıtlaması yoktur.
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_Entities == null)
                {
                    _Entities = _dbContext.Set<T>();
                }
                return _Entities;
            }
            //yada  protected virtual DbSet<T> Entities
            //{
            // get {return _Entities ??(_Entities=_dbContext.Set<T>());}
            //}
        }

    }
}
