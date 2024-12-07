using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.Mappings
{
    //Mapingler veri tabanıyla nesnenin karşılaştırılmasını ilişkilendirmesini sağlar normal şartlarda
    //entityframework bu işlemleri otomatik kendi yapar bizim projemizde product tablosunda ProductId ,
    //CategoryId,ProductName,QuantityPerUnit,UnitPrice SQL de bu Products tablasounda bulunan kolonlarda yani Colomns da
    //yer alır ama bazen türkçe kelimeler yada büyük küçük harf hatası  yapabiliriz ve
    //sql product tablosunda eşleşme sorunu cıkar bu görmek ve düzeltmek için maping yapmamız daha sağlıklı ve güvenilir olur
    //yada Türkçe veritabanıda kullanırsak da yapmak zorundayız.

    //3-Hangi teknolojiyi kullanacaksak örnegin biz EntityFremawork teknolojisini kullanacagız ve onun<Product> nesnesini kullanacagız.

    //EntitiyFrameworkun EntitiyTypeConfiguration jenerik sınıfını kullanıyoruz. 
    public class ProductMap:EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            //Tablo olarak@ "Products" şema olarak ise @"dbo"kullanırız.
            ToTable(@"Products", @"dbo");
            //Entity Framework Core Fluent API HasKey yöntemi, bir varlığı ( EntityKey) benzersiz
            //olarak tanımlayan ve bir veritabanındaki Birincil Anahtar alanına eşlenen
            //özelliği belirtmek için kullanılır
            HasKey(x => x.ProductId);
            //Product sınıfında yer alan ProductId Veri tapanındada( Sql )de ProductId karşılık geliyor ve ilgili kolonlara bağlıyoruz.
            Property(x => x.ProductId).HasColumnName("ProductId");
            //Product sınıfında yer alan CategoryId Veri tapanındada( Sql )de CategoryId karşılık geliyor ve ilgili kolonlara bağlıyoruz.
            Property(x => x.CategoryId).HasColumnName("CategoryId");
            //Product sınıfında yer alan ProductName Veri tapanındada( Sql )de ProductName karşılık geliyor ve ve ilgili kolonlara bağlıyoruz.
            Property(x => x.ProductName).HasColumnName("ProductName");
            //Product sınıfında yer alan UnitPrice Veri tapanındada( Sql )de UnitPrice karşılık geliyor ve ve ilgili kolonlara bağlıyoruz.
            Property(x => x.UnitPrice).HasColumnName("UnitPrice");
            //Product sınıfında yer alan QuantityPerUnit Veri tapanındada( Sql )de QuantityPerUnit karşılık geliyor ve ve ilgili kolonlara bağlıyoruz.
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit");
        }
    }
}
