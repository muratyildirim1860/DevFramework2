using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Logging.Log4Net.Logger
{
    //4

    //Şimdi loggerlarmızı yazalım yani hangi ortamlarda loglama yapmak istiyorsak onlarla ilgili temel configarasyonlarımızı yapacagız.
    //Loglama yapmak istedimiz classlar için bir databaseLogger oluşturuyorum.
  public   class DatabaseLogger:LoggerService
    {
        //logger bilgisini configarasyon  dosyasında yazacagım  DatabaseLogger isimli logger dan al.Yani biz DatabaseLogger için base
        //LogManager.GetLogger çagırarak "DatabaseLogger" çagırarak ordaki implamentasyonu kullanıp onu base yoluyoruz oda bizim 
        //LoggerService de yazdıgımız ilgili loglama işlemlerini gercekleştirecektir.
        public DatabaseLogger() : base(LogManager.GetLogger("DatabaseLogger"))
        {

        }
    }
}
