using DevFramework.core3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.DataAccess
{
 public interface IQueryableRepository<T>where T:class,IEntity,new()
    {
        //Context in kapanmaması için bir QureyableRepository uyguluyoruz.Opersayonlar select olacagı için tek bir imza(implemetasyon) olacak.
        //IQeryable Contecxt ti kapatmadan 1 den fazla sorguyu uygulamak için kullanırız.

        
        IQueryable<T> Table { get; }
    }
}
