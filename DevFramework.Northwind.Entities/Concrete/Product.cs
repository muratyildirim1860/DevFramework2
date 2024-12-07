using DevFramework.core3.Entities;

namespace DevFramework.Northwind.Entities.Concrete
{
    public class Product:IEntity
    {
        //concrete=somut
        //1-Veritabanındaki Product teşkil ediyoruz.
        //Entities katmanımızın altına bir klasör oluşturuyorum.
        //Adını “Concrete” veriyorum. “Concrete” klasörü altında somut nesnelerimiz olacaktır.
        //NHibernate kullanma olasılıgına göre virtual yapıyoruz.
        public virtual int ProductId { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string QuantityPerUnit { get; set; }
        public virtual decimal UnitPrice { get; set; }
    }
}
