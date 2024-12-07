using DevFramework.core3.DataAccess.NHhibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers
{
    // helper bizim kodlamayı yapacagımız veri tapanına ilgili NHİBERNATE configurasyanunu içerir.
    public class SqlServerHelper : NHibernateHelper
    {
        //protected abstract ISessionFactory InitializeFactory(); ezip o veri tabanına özgü bir SessionFactory oluştururuz. 
        protected override ISessionFactory InitializeFactory()
        {
            //  Fluently.Configure kullana bilmemiz için  Nuget paketlerin den FluentNhibernate DevFramework.Northwind.DataAccess den indirmemiz lazım .
            //Database olarak biz MsSqlConfiguration.MsSql2012 kullanacagız.
            
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("NorthwindContext")))
                //Projede entity sınıflarımıza denk gelen mapping sınıflarını otomatik yakalamak için gereklidir.
                //Mappingleri nerden alacagımızı şu şekilde yaparız .Mappingleri Asseblyden buluyoruz  Dataacces katmanına git bak ve ordan maping özeligi olanı bak uygula.
                .Mappings(t => t.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())).BuildSessionFactory();
        }
      
    }
}
