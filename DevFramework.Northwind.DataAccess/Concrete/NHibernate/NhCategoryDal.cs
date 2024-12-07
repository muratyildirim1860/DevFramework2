using DevFramework.core3.DataAccess.NHhibernate;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate
{
    //NHibernateHelper nesnesini Dependency Injection ile talep ederek ilgili controllerda gerekli veritabanı işlemlerini gerçekleştiriyoruz.
    //böylece veritabanımız üzerinde işlemlerimizi aşagıdaki kodlarla gerçekleştirmiş bulunmaktayız.
    public class NhCategoryDal: NhEntityRepositoryBase<Category>, ICategoryDal
    {
        public NhCategoryDal(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {

        }
    }
}
