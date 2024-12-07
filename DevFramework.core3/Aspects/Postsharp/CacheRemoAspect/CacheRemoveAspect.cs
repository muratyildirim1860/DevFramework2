using DevFramework.core3.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.Aspects.Postsharp.CacheRemoAspect
{
    [Serializable]
    public class CacheRemoveAspect : OnMethodBoundaryAspect
    {
        //Burada cacheden silmek için 2 yönetemimiz olacak.1 incisi bir Manager deki bütün cacheleri silmek 2.cisi bizim verdigimiz
        //paterne uygun yani bizim verdigimiz cacheleri sil diyebilecegimiz ortam.

        //öncelikle biz bir _pattern gönderebilecegiz
        //sonra _cacheType gönderebiliriz.
        //Birde IcacheManager gönderiyorum.

        //Bu işlemde benim 2 tane consracerim(ctor) olacak
         
        private string _pattern;
        private Type _cacheType;
        private ICacheManager _cacheManager;
        //1.concracerim(ctor) da sadece cacheType isteyecem o cacheType uygun olarak silme işlemlerini gercekleştirecegiz.
        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }
        //2.concracerim(ctor) da string  patten  isteyecem type cacheType türünde vererek işlemi gercekleştirecegim.
        public CacheRemoveAspect(string patten, Type cacheType)
        {
            _pattern = patten;
            _cacheType = cacheType;
        }
        
        public override void RuntimeInitialize(MethodBase method)
        {
            //kişi bana yanlış bir cache mekanizması gönderebilir if diyerek.gönderilen _cacheType bir ICacheManager türünde degilse
            //throw new Exception olarak işlemi sürdür.
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }
            //_cacheManager için bir instance üretecegiz gönderdigim hersey bir ICacheManager oldugu için ben onu atayabilirim
            //Activator yaparak bir sınıf örnegi oluşturabiliyoruz.Activator.CreateInstance gönderilen _cacheTypes için bir instance üretmiş olduk
            //ve daha sonra bunları kullacabilecegim.
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);
            base.RuntimeInitialize(method);
        }
        //Biz cacheden kaldırma işleminde örnegin cachede bizim bir ürün listemiz var yeni ürün eklendiginde ,ürün güncellendiginde 
        //veya bir ürün silindiginde dolasıyla bizim cache kaldırmamız lazım çünkü cache değişmiş oldu.Bu bakımdan bu operasyon başarılı oldugunda
        //yani ekleme,güncelleme ve silme operasyonları benim cachden datayı silmem lazım 
        //cache data ekleme işlemlerini geliştirecek asbectleri yazacagız aşagıdaki kodla.Bu metod calıştırıldıgında metodun için girmeden yani
        //metodumuzu oveirred edip işlemleri gercekleştirecegiz.
        public override void OnSuccess(MethodExecutionArgs args)
        {
            
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern) ? string.Format("{0}.{1}.*",
                args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name) : _pattern);
        }

    }
}
