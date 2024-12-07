using DevFramework.core3.CrossCuttingConcerns.Logging;
using DevFramework.core3.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Constraints;

namespace DevFramework.core3.Aspects.Postsharp.LogAspects
{
    [Serializable]
    //ek olarak bu asbectler class üstüne  konuldugunda loglama bunuda gerektirebilir ya uygulamanın tamamında veya bir sınıfın tamamında
    //loglama yapmak gibi. yani  aspecti alıp productManager da en tebeyede koyabiliriz  [LogAspect(typeof(DatabaseLogger))].
    //dolasıyla public class ProductManager : IProductService en tebesine yazarsak [LogAspect(typeof(DatabaseLogger))] consakcırlarda(ctor) 
    //aspecten yararlanır ama biz consakcırın loglanmasını istemiyoruz bu acıdan MulticastAttributeUsage kullanacagız.
    //MulticastTargets.Method diyerek tümüne degilde sadece Instance(Bir class'tan türetilen nesnedir) larında kullanacagız yani bu şu anlama geliyor
    //nesne Instance(Bir class'tan türetilen nesnedir) larının örneklerinin method larına uygula yani hic bir şekilde konsrakcıra uygulama diye bir kural ortaya cıkarıyoruz.
    [MulticastAttributeUsage(MulticastTargets.Method,TargetExternalMemberAttributes =MulticastAttributes.Instance)]
    
    public class LogAspect: OnMethodBoundaryAspect//Aspect olabilmesi için OnMethodBoundaryAspect den türetmem gerekir.
    {
        private Type _loggerType;//Bizim kullanımda  neye göre loglayacagımız databaseloggera söylememiz gerekiyor onun için 1 adet Type ihtiyacım var.
        //Burda _loggerType  databaselogger,filelogger,eventlogger veya concsollogger vs biri olabilir.

        private LoggerService _loggerService;//Şimdi benim _loggerType çalıştırabilmem için yani o databaseLogger çalıştırmam için
        //onun bir instance nı üretmem gerekir.Yani database logger gelebilir file logger gelebilir console logger yazdıgımızda gelebilir
        //event logger gelebilir yani gelebilecek loggerların türü cok fazla ozaman benim yapmam gereken nesnel çalışmak için
        //ve benim bir loggerServicesim var ve bu gelen nesneyi _LoggerService atayabilmeliyimki hangi loggerla çalışacagımı biliyimki ve uygulasın.
        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }
        //Bir instance oluşturmam gerekiyor   bunun içinde bir instance  üretmek için RuntimeInitialize yaralanırız
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType!=typeof(LoggerService))//Bu loggger LoggerService implente olmuyorsa yani kullanıcı buraya databaselogger yada fille loger dışında 
             //Bir logger gönderdiyse orda throw new Exception("Wrong logger type"); yazarak sorunu cözmeye calıştık.
            {
                throw new Exception("Wrong logger type");
            }
            //new” operatörü olmaksızın “Activator” sınıfı üzerinden aşağıdaki gibi instance talep edebilmekteyiz.
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);//Eger herşey yolundaysa bana LoggerService için bana 
            //1 tane instance üretsin 
            base.RuntimeInitialize(method);
        }
        //Burda biz logları info formatında yapacagız.yani bunları sadece metoda girildi bu metodu şu kişi şu saat de şu datalarla çagırdı
        //şeklinde tutmak istiyoruz.
     
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!_loggerService.IsInfoEnabled)//Eger _loggerService de IsInfoEnabled durumda degil  ise loglama işlemini gercekleştirme.
            {
                return;
            }
            //Eğer ben loglama yaparken  hata vermesin işlem devam etmesini istersem
            try
            {
                //Aksi takdirde benim loglama işlemini gerceklestirmem gerekecek.Bunun için öncelikle loggun parametrelerine ulaşmam gerekiyor
                //argümanun parametrelerini al(args.Method.GetParameters()) ve onları select ve her bir degeri logParameters isimlerini tutacak bir listeye
                //ata.Her bir parametre için LogParameter si oluştur.

                //Burda bizi t=tipimiz yani Type i=Iterator yani 0 argüman 1 inci argüman 2 argüman gibi her select oparesyonu için bir i atıyor.
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
                {
                    Name = t.Name,//Burda LogParametersinin  ismi t burda type karşılık geliyor burda örnek Name şehir gibi alırsak
                    Type = t.ParameterType.Name,//burdaki kod da  type ise şehir olan Name in  Type string oldugunu gösteriyor.
                    Value = args.Arguments.GetArgument(i)//Burda ise parametrenin değerine karşılık geliyor
                                                         //Yukardaki kodda her bir parametreleri alıp  listeye eklemiş oluyorum.
                }).ToList();
                //Şimdi benim bu parameterleri bir logDetaile atmak gerekecek.
                //LogDetail ise bizim sınıfımız namespace miz metod ismimiz onlara karşılık geliyor.Biz aşagıda sınıfı(FullName) ve metod ismini
                //(MethodName) alacagız.
                var logDetail = new LogDetail
                {
                    //Method.DeclaringType null diye bir bak eğer null sa null olarak kabul et onu 
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,//Metodun ismi
                    Parameters = logParameters
                };
                _loggerService.Info(logDetail);//Bizim bu log işlemlerimizi gercekleştirmemiz gerecek biz info olarak uyguylacagız.
            }
            catch (Exception)//Hata oldugunda da herhangi birsey yapmasını istemiyorum.
            {

              
            }
            
        }
    }
}
