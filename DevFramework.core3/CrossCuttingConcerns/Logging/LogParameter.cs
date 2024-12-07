using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Logging
{
   

    //Loglama için egitimizmizde en popiler olan Log4Net kullanacagız.Loglamayı nasıl yapacagımız cok önemlidir örnegin ProductManager da 
    //update metodunu ele alırsak ve loglamak istersek bunun için benim kim hangi metodu hangi parametrelerle kim ne zaman çalıştırdı gibi
    //bir bilgiyi tutmamız gerekir.Bunuda dolaysıyla benim logu tutacak datayı yönetebilecegim bir nesneye ihiyacımız var buda LogParameter sınıfıdır.

  public  class LogParameter
    {
        //peki biz bu logparameterde ne tutacagız?Burada bizim hangi log datasını tututumuzla alakalıdır.Fakat özelikle metodla ilgili kısımda
        //metodun ismi ,metodun tipi ve ek olarak o metod içeresinde gecen parametreler ve o parametrelerin tipleride çok önem arz eder.
        //Dolasıyla bu logparametresinin içerisinde metod parametresinin ismi,type ve degerini tutuyor olacagız.
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
    }
}
