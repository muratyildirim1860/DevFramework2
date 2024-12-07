using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Logging.Log4Net
{
    //1

    //ILog la interface dedigimiz bütün loglar alınır.Hangi log lamaya ihtiyacımız varsa bunu enjekte ederiz ve bu şekilde loglamayı gercekleştiriz
    //yani bu loglama yöntemi veri tabanına olabilir,bir metin dosyasına yazılıyor olabilir,Json formatında,XML formatında bir event viewer,
    //oracel sql servere tabanına yazılabilir,bir output da yada consola da yazılabilir
    //dolaysıyla bizim önçelikle Log4Netin ILog interfacesini oluşturmamız gerekiyor.Bunun için ben LoggerService denen bir sınıf yazıyorum.



    //LoggerService neden yapıyorum sorusunun cevapı:Farklı ortamlarda farklı loglama tekniklerini kullanıyor olabilirim.Örnegin veritabanına
    //sadece fatall ve info seviyesindeki logları yazmak istersem veya çalışma ortamında debug loglarını veya uyarı loglarını gecirmek isteyebilirim.
    //Bu baglamda bunları yönetebilecegim LoggerService vasıtasıyla bunu yapacagız.
    [Serializable]
    public class LoggerService
    {
        //ILog log4net kütübanesinden devframeworkcore3 de kütübaneyi yüklememiz lazım.
        private ILog _log;

        public LoggerService(ILog log)
        {
            //depency enjection yönetemi ile ILoge ekliyoruz.
            _log = log;
        }
        //Şimdi uygulamamızda bir kactane kural geliştirecegiz yani loglama ortamında info logları(bigi logları) açıkmı bunu uygulayacagız.


        //Burada  normal klasik loglama yöntemleri içinse yani şu saatde şunu çagırdı
        //şu metodu şu parametrelerle çagırdı gibi sonuclar içinse   IsInfoEnabled kullanabiliriz.Bu tamamen bizim logu tutma şekilimizle alakalıdır.
        public bool IsInfoEnabled//Bu loglama ortamında info logları acıkmı.
        {
            get { return _log.IsDebugEnabled; }
        }
        
        public bool IsDebugEnabled///Bu loglama ortamında Debug logları acıkmı.
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }
        
        public bool IsErrorEnabled//bizim uygulamamızda en cok kullacagımız loglama error dur .mesala bir hata oldugunda IsErrorEnabled calıştırırız
        {
            get { return _log.IsFatalEnabled; }
        }

        //Bizim bu info,debug,warn,fatal ve error mesajlarını loglamamız gerekiyor.Bu servisce vasıtasıyla
        //Info vasitasıyla yapacagız.

        
        public void Info(object logMessage)//bana bir logMessage nı ver  ben onu Info şeklinde veritabanına veya nereye logluyacaksam logluyum.
        {
            
            if (IsInfoEnabled)//Eğer infoEnable ise loglama işlemini gercekleştir.
            {
                //yani bu logmessage al ilgili yere ekle 
                _log.Info(logMessage);
            }
        }
        public void Debug(object logMessage)
        {
            
            if (IsDebugEnabled)//Eğer IsDebugEnabled ise loglama işlemini gercekleştir.
            {
                //yani bu logmessage al ilgili yere logla 
                _log.Debug(logMessage);
            }
        }
        public void Warn(object logMessage)
        {
            
            if (IsWarnEnabled)//Eğer IsWarnEnabled ise ozaman uyarı formatında  loglama işlemini gercekleştir.
            {
                //yani bu logmessage al ilgili yere logla 
                _log.Warn(logMessage);
            }
        }
        public void Fatal(object logMessage)
        {
            
            if (IsFatalEnabled)//Eğer IsFatalEnabled ise fatal olarak bunu kaydet loglama işlemini gercekleştir.
            {
                //yani bu logmessage al ilgili yere logla 
                _log.Fatal(logMessage);
            }
        }
        public void Error(object logMessage)
        {
            //Eğer IsErrorEnabled ise hata mesajları acıksa  eror formantında bunu loglama işlemini gercekleştir.
            if (IsErrorEnabled)
            {
                //yani bu logmessage al ilgili yere logla 
                _log.Error(logMessage);
            }

        }
        //Bu yaptıgım yukardaki işlemlerde uygulamalarımızda hata formatında hata olarak neler oluşmuş  diye filirtleyebiliriz veya bu uygulamada
        //kimler hangi metodu çagırmış diyip info formantında bunları çagırabilriz.
    }
}
