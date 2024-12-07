using DevFramework.core3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.DataAccess
{
    //where koşulunu getirerek (T) kıstalamalar uyguluyoruz bizim programımızı kullanan programcılar hata yapmamasını sağlıyoruz.
    //new()=class gönderir ve  abstract metod bile gönderse new() lenemiyecek. 
    //class=referans tiptir yani interface ve abstractlarda birer referans tiptir.
    //IEntity=Bir imzadır.yani ben veri tabanı nesnelerimi IEntity süslüyor olacagız.
    //T=jenerik olarak çalışması için veriyoruz.
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        // datanın tümünü yada datanın where koşuluyla getirmesini istiyorum onun için Expression dan func görderiyoruz.

        //filtre null boş gönderirse  datanın tümünü getirecek.

        //Sadece filter gönderirse datanın filterelenmiş şeklini gönderecek.

        //List lerle calıştıgımız zaman context açılıp kapanır.Dolasıyla ToList opersayonu gördügünde veri tabanıyla olan
        //bütün context sonlanmış olur ve kapanır.

        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T Add(T entity);
        T Update(T entity);
        //delete işleminde primer2 anahtarına göre siliyoruz.
        void Delete(T entity);

    }
}
