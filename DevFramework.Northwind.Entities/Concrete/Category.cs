using DevFramework.core3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Entities.Concrete
{
 public class Category:IEntity
    {
        //Entities katmanımızın altına bir klasör oluşturuyorum. Adını “Concrete” veriyorum.
        //“Concrete” klasörü altında somut nesnelerimiz olacaktır.
        public virtual int CategoryId { get; set; }

        public virtual string CategoryName { get; set; }

    }
}
