using DevFramework.core3.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Constraints;

namespace DevFramework.core3.Aspects.Postsharp.ValidationAspects
{
    
    [Serializable]
    [Protected]
    [Internal]
    //OnMethodBoundaryAspect: Attribute olarak tanımladığımız bir metodun işlem adımlarını takip edebileceğimiz sınıftır.
    //OnEntry, OnSuccess, OnExit, OnException override metodları ile istediğimiz metodun özelliklerine erişebilmekteyiz.

    //Attribute :uygulandıkları tiplerin ya da üyelerin çalışma zamanındaki davranışlarının değiştirilmesine olanak sağlayan sınıflardır.
    //Niteliklerin class olduğu rahatlıkla söylenebilir.Karar baklavaları olarak tanımlanan, program içerisinde kullanılan "if" bloklarına benzer.
    //Yazılan programlarda tanımlanan metod ya da sınıflardan önce[Conditional(değişken)] (Koşullu) etiketi ile belirlenir.

    //Yani ProductManager sıfımızda yer alan [FluentValidationAspect(typeof(ProductValidatior))] koda dahil olmasını istiyorsak 
    //FluentValidationAspect de uygularızki [FluentValidationAspect(typeof(ProductValidatior))] kodda çalışsın.
    public class FluentValidationAspect:OnMethodBoundaryAspect
    {
        //_validatorType(product,category)
        Type _validatorType;
        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }
        //Metoda girdigimizde doğrulama yapmak istedigimiz için OnEntry kullanıyoruz.
        [Internal]
        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            //_validatorType(product,category)

            //var entityType product argümanıyla çalışmamı sağlıyacak .

            //BaseType(AbstractValidator) GetGenericArguments() bak ve 1 array döndür.
            // _validatorType(product) BaseType(AbstractValidator) ın GetGenericArguments manlarına bak product nesnenin 1 generic argüman tipini al.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            //ProductManager da çalışacagım metodun yani Add(product product)yada update(product product)
            //içindeki entities arayacagım yani product dı arıyorum.

            //arg=Çalıştıralan metodla ilgili bilgi almamımızı sağlar.

            //GetType tüm argümanlarını yani parametrelerini gez  veri tipini al ve o tip eğer entityType eşitse onu entities e ekle.
            //productmanagerman da çalıştıgım metodun parametlerini gezip tipi product olanları yakalıyorum.
            var entities = args.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                //validator=product olabilir
                //entity=parametreyle gelen entity gezecem.
                ValidatorTool.FluentValidate(validator, entity);
            }
        }

    }
}
