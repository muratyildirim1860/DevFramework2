using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings
{
  public  class CategoryMap: EntityTypeConfiguration<Category>
    {

        public CategoryMap()
        {
            //Tablo olarak@ "Categories" şema olarak ise @"dbo"kullanırız.
            ToTable(@"Categories", @"dbo");
            //Entity Framework Core Fluent API HasKey yöntemi, bir varlığı ( EntityKey) benzersiz
            //olarak tanımlayan ve bir veritabanındaki Birincil Anahtar alanına eşlenen
            //özelliği belirtmek için kullanılır
            HasKey(x => x.CategoryId);
            //Category sınıfında yer alan CategoryId Veri tapanındada( Sql )de CategoryId karşılık geliyor ve ilgili kolonlara bağlıyoruz.
            Property(x => x.CategoryId).HasColumnName("CategoryId");
            //Category sınıfında yer alan CategoryName Veri tapanındada( Sql )de CategoryName karşılık geliyor ve ilgili kolonlara bağlıyoruz.
            Property(x => x.CategoryName).HasColumnName("CategoryName");
            
        }
    }
}
