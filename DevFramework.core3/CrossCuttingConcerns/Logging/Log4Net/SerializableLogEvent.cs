using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Logging.Log4Net
{
    //2

    //Loglalarımı tutacagım şekil JSon formatında olacak Json formatına bir nesnenin
    //dönüştürelebilmesi için onu  SerializableLogEvent hale getiriyor olacagım ve [serializable] keyworduyla besliyecem.
    [Serializable]
  public   class SerializableLogEvent
    {
        //Bu SerializableLogEvent de loglama bilgisini gönderiyor olmamız lazım bu loglama bilgisi LoggingEvent dedigimiz
        //Log4net den gelen log datasını kendisini barındırır.
        private LoggingEvent _logginEvent;

        public SerializableLogEvent(LoggingEvent logginEvent)
        {
            _logginEvent = logginEvent;
        }

        //Burda loglama bilgisi olarak Username yani bu log işlemine sebep olan kişi kimdir bilgisini tutmak istiyorum ve onu Read only şekline 
        //dönüştürmek istiyorum.
        public string UserName  {



            get {


                return _logginEvent.UserName;
            
                 }       
        }
        //Log mesajının  yani logdatasının mesajla ilgili kısmını işleme koymak istiyorum yani hangi metodun hangi parametrelerle
        //çalıştırıldıgını ögrenmek istiyorum.
        //Birde messaObjecti Serializable hale getirmek istiyorumki bu bize log4net in LayOutda vs bizden isitiyecegi
        //bir nesne olacakdır.
        public object MessaObject
        {
            get
            {
                return _logginEvent.MessageObject;
            }
        }


    }
}
