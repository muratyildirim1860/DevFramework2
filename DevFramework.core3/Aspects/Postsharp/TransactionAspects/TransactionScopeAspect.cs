 using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.core3.Aspects.Postsharp.TransactionAspects
{
    //Bu işlem ister insert, ister update, ister ise delete işlemi olsun.
    //Bu noktada eğer yapacağınız işlem farklı bir veya bir kaç noktayı etkileyecek ise ve bu noktaların
    //bir tanesinde dahi hata almanız durumunda yapılan tüm işlemleri (o esnada) geri almak istiyorsanız
    //kesinlikle ihtiyaç duyacağınız bir metoddur Transaction kavramı.

    //İşlemlere hiç başlamadan önce bir Transaction açılır ve sizin kod tarafındaki işleminiz
    //(döngü veya ardı ardına gelen farklı insert/update/delete işlemleri olabilir) bitip de
    //siz transaction’ı kapatana (commit) kadar o tabloya farklı uygulamalardan veya noktalardan
    //gelen tüm talepler kısa süreli beklemeye alınır ve sizin işleminiz daha uzun sürecek ise
    //timeout verilerek geri çevrilir. İşte bu da, aynı tablo veya kayıt üzerinde birden fazla
    //kişinin aynı anda işlem yapmasının önüne geçmiş olur.

    //serializable=Bir nesnedeki verinin bir yerde depolaması veya ağ ortamında bir yerden bir yere gönderilmesi gerektiği
    //durumlarda uygun formata dönüştürülmesi işlemine serileştirme denir. Serileştirilen nesneler veritabanı, hafıza veya
    //dosya gibi ortamlarda saklanabilirler.

    //OnMethodBoundaryAspect=Sınıfın yeni bir örnegini başlatır.
    [Serializable]
    public class TransactionScopeAspect:OnMethodBoundaryAspect
    {
        //Farklı parametrelerle Transactionlarımızı cağırmak isteyebiliriz bunun içinde TransactionScopeOption kullanırız.
        private TransactionScopeOption _option;
        //TransactionScopeAspect parametrelisi kullnacaksak aşagıdaki metod içine kodlarımızı yazarız.
        public TransactionScopeAspect(TransactionScopeOption options)
        {
            _option = options;
        }
        //TransactionScopeAspect parametresiz kullanacaksak sadece metodu acar bırakırız yazılacak kod varsa metod içine yazarız.
        public TransactionScopeAspect()
        {

        }
        //Metoda girildiğinde benim bir OnEntry açmam gerekiyor.Dogrulama yaptıgımızda
        public override void OnEntry(MethodExecutionArgs args)
        {
            //metoda girildiginde benim bir TransactionScope acmam gerekiyor.eger varsa parametre olarak _option buraya getirecek yoksa null olacak.
            args.MethodExecutionTag = new TransactionScope(_option);
        }
        //Metod başarılı oldugu zamanda yani try içerisinde ( OnSuccess) oldugunda.yani try içerisi OnSuccess oluyor.
        public override void OnSuccess(MethodExecutionArgs args)
        {
          
            ((TransactionScope)args.MethodExecutionTag).Complete();
        }
        //Metod başarılı degilse yani try(OnSuccess) oldugunda OnExit olacak.
        public override void OnExit(MethodExecutionArgs args)
        {
            ((TransactionScope)args.MethodExecutionTag).Dispose();
        }

    }
}
