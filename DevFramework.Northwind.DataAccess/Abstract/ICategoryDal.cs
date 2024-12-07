using DevFramework.core3.DataAccess;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Abstract
{
    //Abstract klasörü oluşturuyorum burada soyut nesnelerimiz olacak (Abstract Class, Interface)gibi
    public interface ICategoryDal: IEntityRepository<Category>
    {
    }
}
