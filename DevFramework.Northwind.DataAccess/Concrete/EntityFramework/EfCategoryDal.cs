using DevFramework.core3.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    //NorthwindContext nesnesini Dependency Injection ile talep ederek ilgili controllerda gerekli veritabanı işlemlerini gerçekleştiriyoruz.
    //böylece veritabanımız üzerinde işlemlerimizi aşagıdaki kodlarla gerçekleştirmiş bulunmaktayız.
    //Concrete klasörü oluşturuyorum burada ise somut nesnelerimiz class’larımız olacak.
    public class EfCategoryDal: EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {
    }
}
