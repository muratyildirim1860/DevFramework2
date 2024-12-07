using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Logging
{
    

    //LogParameter  ek olarak önemli bir nesnemizde LogDetail dir.LogParametresi sadece metodun names,type ve parametresini tutuyordu.
    //LogDetail ise hangi namespace deki hangi sınıftaki metod ve onun parametreleri. 
  public   class LogDetail
    {
        //Bir metodun bilgilerini tutacagız bu namespacesde barından sınıfa karşılık gelecek FullName. 
        public string FullName { get; set; }
        //Burda ise metodun ismini yani o namespacasdeki yani class daki daha dogrusu hangi metod
        public string MethodName { get; set; }
        //Bu metodun parametreleri olabilir ama bir metodun birden fazla parametreleri olabilir onun için List<LogParameter> kullanırız.
        public List<LogParameter> Parameters { get; set; }
    }
}
