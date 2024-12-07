using DevFramework.core3.DataAccess.NHhibernate;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
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

    public class NhProductDal:NhEntityRepositoryBase<Product>,IProductDal
    {
        private NHibernateHelper _nhibernateHelper;
        public NhProductDal(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
            _nhibernateHelper = nHibernateHelper;
        }

        public List<ProductDetail> GetProductDetails()
        {
            using (var session= _nhibernateHelper.OpenSession())
            {
                var result = from p in session.Query<Product>()
                             join c in session.Query<Category>()
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetail
                             {
                                 ProductId = p.ProductId,
                                 CategoryName = c.CategoryName,
                                 ProductName = p.ProductName
                             };
                return result.ToList();
            }
        }
    }
}
