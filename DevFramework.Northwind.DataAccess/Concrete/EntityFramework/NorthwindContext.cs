using DevFramework.Northwind.DataAccess.Concrete.Mappings;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
  public  class NorthwindContext:DbContext
    {
        //4-Category ve Product için Dbset oluşturuyoruz.

        //Bir “NorthwindContext” adında sınıf oluşturuyorum ve “DbContext”’den inherit(türetmek) ediyorum.

        //Sonra NorthwindContext sınıfımıza ait constructor method oluşturuyorum .

        ////Eğer veritabanımız oluşmamışsa  ilk çalışmada oluşturulması için
        //bir Database Initializer ekliyorum.

        //Burda ben Northwind veritabanıyla çalıştıgım  için  SetInitializer<NorthwindContext> kodunu yazarak hazır bir veri tabanı ile
        //çalıştığımızı gösterip ve Null yaparak da veri tabanın da veri oluşturmasını engellimiş oluyoruz.
       


        //Ardından generic dbset Product ve dbset Category oluşturuyorum.
        //Bunun için “DevFramework.Northwind.Entities” projemizi referans eklemek durumundayız. Ardından bir Products ve Categories
        //nesnelerini oluşturuyorum.
        public NorthwindContext()
        {
            Database.SetInitializer<NorthwindContext>(null);
        }


        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
        }

    }
}
