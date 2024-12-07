using DevFramework.core3.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.DataAccess.EntityFramework
{
    //2 tane nesne tanımlıyoruz ilki TEntity yada (T) digeride  TContext dir .
    //TEntitiy yada(T) veri tabanında çalışacagımız(veritabanı nesnemizdir(product,employess,categories)).
    //TContext sınıfı genel anlamda veri tabanı işlemlerinin halledildiği sınıftır.

    //Context Sınıfı İşlevleri

    //Veritabanı bağlantılarını  yönetir.
    //Model ve ilişkileri ayarlar.
    //Veritabanı sorgulama.
    //Değişikleri yönetir.
    //Caching  Transaction yönetimi
    //Object Materialization(Veritabanından aldığı verileri entity nesnelerine dönüştürür).


    //EfEntityRepositoryBase veritabanına veri ekleme, güncelleme ve okuma gibi
    //CRUD (Create, Read ,Update, Delete)
    //işlemlerimiz için oluşturmuş olduğumuz kodların tekrar kullanılabilirliğini sağlamaktır.
    //DBContet veritabanımızla uygulamamız arasında sorgulama,
    //güncelleme, silme gibi işlemleri yapmamız için olanak sağlar. Yani veritabanı içinde yer alan verilerimizle alakalı olarak
    //her türlü süreçte iletişimimizi sağlayan bir classtır.

    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
         where TEntity : class, IEntity, new()
         where TContext : DbContext, new()

    {
        public TEntity Add(TEntity entity)
        {
            //TEntity mizin TContext’ e eklendiği anda oluşan durumdur. TEntity üzerinde var olmayan yeni bir kayıt oluşmuştur, yani insert işlemi yapılmıştır.
            //Insert işlemi henüz veritabanına yansıtılmamıştır, SaveChanges işlemi ile veritabanına da yansıyacaktır
            
            //Using tanımlanan bir nesnenin dispose edilmesini garantilemek yani ram’den atılmasını garantilemesidir.
            using (var context=new TContext())
            {
                //update delete insert için context de  ilgili nesneye abone olmam gerekiyor.
                //Durumunu eklenecek data olarak EntityFrameworka  bildiriyorum.
                //EntityState kısaca Entity’imizin o an ki durumunu bildiren bir propertydir.
                //entity üzerinde yapılan çeşitli işlemler sonrası durumu değişmektedir.

                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                //Ekledigim nesneyi(entity) de return ediyorum.
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context=new TContext())
            {
                //EntityState kısaca entity’imizin o an ki durumunu bildiren bir propertydir.
                //entity üzerinde yapılan çeşitli işlemler sonrası durumu değişmektedir.

                var deletedEntity = context.Entry(entity);               
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
                
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context=new TContext())
            {
                //Tek bir nesne döndürecegim için SingleOrDefault gönderiyorum.
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            //hangi contexti gönderirsek o TContexti acacak.
            using (var context=new TContext())
            {
                //context.Set ile ilgili TEntity e abone oluyorum ve filtre göndermedigi için datanın tümünü göndeririyoruz.
                
                return filter == null ? context.Set<TEntity>().ToList() :
                //şayet filitre doluysa ilgili TEntitye abone oluyorum ve wherele işlemi devam ediyorum.
                    context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            //EntityState kısaca Entity’imizin o an ki durumunu bildiren bir propertydir.
            //Entity üzerinde yapılan çeşitli işlemler sonrası durumu değişmektedir.
            using (var context=new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }
    }
}
