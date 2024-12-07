﻿using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevFramework.DataAccess.Tests.NHibernateTests
{
    [TestClass]
    public class NHibernateTest
    {
        //NhProductDal göre bütün ürünleri listelemek yada filtrelemek için birim test uygulamasıyla deniyoruz.
        [TestMethod]
        public void Get_all_returns_all_prdoucts()
        {
            NhProductDal productDal = new NhProductDal(new SqlServerHelper());

            var result = productDal.GetList();
            //Beklenen 77 gelen içinde resut.Count parametresiyle görüyoruz.
            Assert.AreEqual(77, result.Count);
            //Veri tabanına bağlanmam gerekecek testi çalıştırmam için DevFramework.DataAccess.Tests mause sağ tıklayıp Ekle diyip yeni öğe diyip ordan Uygulandırma yapılandırma dosyasını tıklarız.



        }
        [TestMethod]
        public void Get_all_with_paremeter_returns_filtered_prdoucts()
        {
            NhProductDal productDal = new NhProductDal(new SqlServerHelper());

            //Parametre yazarak listeleme için bir test yapıyoruz.veri tabanında  products tablasonda ProductName de içinde 'ab' olan 4 tane veri vardır.
            var result = productDal.GetList(p => p.ProductName.Contains("ab"));
            //Beklenen 77 gelen içinde resut.Count parametresiyle görüyoruz.
            Assert.AreEqual(4, result.Count);
            //Veri tabanına bağlanmam gerekecek testi çalıştırmam için DevFramework.DataAccess.Tests mause sağ tıklayıp Ekle diyip yeni öğe diyip ordan Uygulandırma yapılandırma dosyasını tıklarız.

            //Veri tapanına baglanabilmemiz içinde app.configing ekliyoruz.



        }
    }
}
