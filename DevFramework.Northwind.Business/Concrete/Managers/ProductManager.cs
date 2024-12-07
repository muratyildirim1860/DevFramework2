using AutoMapper;
using DevFramework.core3.Aspects.Postsharp.AuthorizationAspects;
using DevFramework.core3.Aspects.Postsharp.CacheAspects;
//using DevFramework.core3.Aspects.Postsharp.CacheRemoAspect;
using DevFramework.core3.Aspects.Postsharp.LogAspects;
using DevFramework.core3.Aspects.Postsharp.PerformanceAspects;
using DevFramework.core3.Aspects.Postsharp.TransactionAspects;
using DevFramework.core3.Aspects.Postsharp.ValidationAspects;
using DevFramework.core3.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.core3.CrossCuttingConcerns.Logging.Log4Net.Logger;
using DevFramework.core3.Utilities.Mappings;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FlentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace DevFramework.Northwind.Business.Concrete.Managers
{

    //Concrete klasörün içinde İş sınıflarımızı oluşturacagız.
    public class ProductManager : IProductService
    {
        //Veri erişiminde(DataAccess) alt katmanına erişim gerekiyor bunun içinde alt katmanda olan IProdctDal kullanıp
        //(EfProductDal,EFCategoryDal,NhCategoryDal ve NhProductDal)
        //süreclerini kullanabilmemizi sağlıyacak olan ProductManager sınıfımızda kullanıyoruz.

        //**Yani ProductManagera alt katmanların ( asbtrack) metodlarını kullanırız bizde
        //IProductDal(EfProductDal,EFCategoryDal,NhCategoryDal ve NhProductDal)
        //kullanarak bu işlemi gercekleştirmesini sağlıyoruz.

        //“ProductManager” sınıfımızda ise “IProductDal” tipinde bir field tanımlıyoruz ve “ProductManager” sınıfımızın
        //constructor methoduna enjekte ediyoruz. Burada Dependency Injection Design Pattern(new anahtar sözcügünü kullanmamamızı saglar)’ni Constructor
        //Based Dependecy Injection tekniği ile uyguluyoruz.

        //**Dependency Injection Design Pattern:
        //Yani, temel olarak oluşturacağınız bir sınıf içerisinde başka bir sınıfın nesnesini kullanacaksanız new anahtar sözcüğüyle
        //oluşturmamanız gerektiğini söyleyen bir yaklaşımdır. Gereken nesnenin ya Constructor’dan ya da Setter metoduyla parametre
        //olarak alınması gerektiğini vurgulamaktadır. Böylece iki sınıfı birbirinden izole etmiş olduğumuzu savunmaktadır. Ha doğru mudur?
        //, dibine kadar doğrudur…
        //Dependency injection kaba tabir ile bir sınıfın/nesnenin bağımlılıklardan kurtulmasını amaçlayan ve o nesneyi olabildiğince
        //bağımsızlaştıran bir programlama tekniği/prensibidir.



        //“IProductDal” üzerinden yani soyut nesnemiz üzerinden gitmekteyiz. Burada ne “EfProductDal” ne de
        //“AdoNetProductDal” tipinde bir nesne veriyoruz. Hangi erişim teknolojisi ile çalışacağımız şu anda belli değildir.
        //Peki nasıl belirleyeceğiz? Burada IoC (Inversion of Control) Container dediğimiz frameworkler devreye girmektedir.
        //IoC Container’lar nesne oluşturma sürecini bizim yerimize gerçekleştiren yapılardır.
        //Ben bu projede Ninject kullanacağım.Ninject .NET Framework’de kullanılan açık kaynak IoC Container’dır.

        

        private  IProductDal _productDal;

        private readonly IMapper _mapper;
        private IProductDal @object;

        // private readonly IQueryableRepository<Product> _queryable;

        //public ProductManager(IProductDal productDal,IQueryableRepository<Product>queryable)

        //ProductManager(IProductDal productDal) based dependency injection özelliklerinden birisi sıkı olan bağımlılığı gevşek bağlı hale getirmekle
        //birlikte Unit Test işlemlerinde, Moq gibi frameworkler ile testlerimiz için orijinal nesnelerin yerine geçecek olan
        //sahte nesneler üretmemize olanak sağlaması.

        //Business katmanında başka bir katmana erişmek için örnegin Date acces layere erişmek için(DAL) businessin konsakşır(ctor) blogunda
        //bir IProductDal enject ni gercekleştiriyorum.Yani ProductManager bana verilen bir IProductDal türündeki nesneye göre işlemlerimi gercekleştirecem.
        //buda beni DAL calışacagım için herhangi bir ORM aracına veya bir DataAcces teknolojisine baglı kalmamamı saglıyor.

        public ProductManager(IProductDal productDal,IMapper mapper)
        {
            _productDal = productDal;
           _mapper= mapper;

            //_queryable = queryable;
        }

        public ProductManager(IProductDal @object)
        {
            this.@object = @object;
        }

        //ürünle birlikte bu ürünle ile tüm cacheleri siliyorum.Buna bir çok yerde ihitiyacımız olacaktır özelikle bir ürünü
        //cok ekleyen ortamlar varsa  cacheden kaldırmayı tercihedebilriz yada cok sık yapamdıgınız ekleme işini günceleme işini yapmıyorsanız
        //yine tercih edebiliriz.
        [FluentValidationAspect(typeof(ProductValidatior))]
        //[CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(FileLogger))]
        public Product Add(Product product)
        {
            //ValidatorTool kurallarından gecip gecmedigini görmek için çagırıyoruz.
            //ValidatorTool.FluentValidate(new ProductValidatior(), product);
            
            return _productDal.Add(product);
        }
        //Ben GetAll metodunu cachelemek istiyorsam bunun için  [CacheAspect(typeof(MemoryCacheManager))] kullanıyorum ve hangi yönetemi 
        //kullanarak cachleme yapacagız sorusuna MemoryCacheManager yönetimi kullanarak yapacagımı belirtiyorum.
        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]//filelogger ve databaselogger yaptık temel implamatasyonlarını gercekleştirdik.fakat
        //biz bu loggere nasıl kullanacagız  bunun için biz  productmanager  da kullacagız.örnegin GetAll metodunu loglamak istedigimde
        //[LogAspect(typeof(DatabaseLogger))] kullanarak yani databaseloggere veri tabanına loglarız.istersek fileloggereda kullanabilriz.
        [LogAspect(typeof(FileLogger))]
        [PerformanceCounterAspect(2)]
        [SecuredOperation(Roles="Admin,Editor,Student")]
        public List<Product> GetAll()
        {
            //return _productDal.GetList().Select(p=>new Product {
            // CategoryId=p.CategoryId,
            // ProductId=p.ProductId,
            // ProductName=p.ProductName,
            // QuantityPerUnit=p.QuantityPerUnit,
            // UnitPrice=p.UnitPrice


            //}).ToList();
            //var products = AutoMapperHelper.MapToSameTypeList(_productDal.GetList());
            var products = _mapper.Map<List<Product>>(_productDal.GetList());
           
            return products;
        }
        

        public Product GetById(int İd)
        {
            //p.ProductId ==İd productId si benim gönderdigim id eşit olan ürünü getirmesini istiyorum.
            return _productDal.Get(p => p.ProductId ==İd);
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
      
        public Product Update(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidatior(), product);


            return _productDal.Update(product);
        }
        [TransactionScopeAspect]
        [FluentValidationAspect(typeof(ProductValidatior))]

        public void TransactionalOperation(Product product1, Product product2)
        {
            

                _productDal.Add(product1);

                _productDal.Update(product2);

            //try catch yöntemiyle  metodun içerisine yazmam lazım ve  baya uzun  kod kirliligine yol açar.
            //ondan dolayı daha kolay ve anlaşılır bir kodu uygularız.Bunuda DevFramework.core3 de bulunan [TransactionScopeAspect] sınıfıyla yaparız.

            //using (TransactionScope scope = new TransactionScope())
            //{
            //try
            //{
            //    _productDal.Add(product1);
            //    _productDal.Update(product2);
            //    scope.Complete();
            //}
            //catch 
            //{
            //    scope.Dispose();

            //}
        }
    }    
}
