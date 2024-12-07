 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Caching.Microsoft
{
    //Bir data cahce eklenecek arzu edildiginde diger kullanıcalar eğer cache de varsa cache üzerindeki datayı çekebilecekler.

    //Aoutbutcahcing=Eğerki bir data parametleriyle birlikte cachelenilirse aynı parametlerle aynı data cagırıldıgında tekrardan cache yapar.

    //Cache birden fazla yöntemle yapabilecegimiz için bunun içerisinde hangi cache yönetemini kullacaksam ona göre klasörleme yapacagız.
    //fakat birden çok yöntemle yapabildigim için bütün  cacheler için bir adet Repository oluşturacagız.
    //Bu egitimde ben cachinle ilgili kısmında ben öncelikle cach için  bir repository yani cacheinterfacesi  oluşturacagız bundan sonra
    //hangi yöntemlerle kullanmak istersem onu implemente edebiliriz.Fakat biz bu egitimde ilk etap da memolicache dedigimiz
    //microsoftdatanet.faremework içerisinde default olarak gelen caching alt yapısını kullanacagız.Bu alt yapı data yı bellekte
    //uygulama sunucusunun belleginde tutacak ve uygulama sunucusunun bellegindeki dataya göre bellegini tüketerek diğer kullanıcılarda bu data
    //üzeriden işlem yapabilecek.Fakat ilerleyen zamanlarda siz arzu ederseniz memcache gibi rediscache gibi çok pobiler caching algoritmalarını
    //bu egitimin için bu frameworkun içine dahiledebiliriz.Bu modulde sadece memolicache yani microsfotcache ekleyecegiz
    //Biz burada microsoft un memory cacheni kullacagız.onun için bir adet cache nesnesi üretip(ObjectCache) onunla sistemi yönetecegiz .
    //logbalansik yapıyoruz farklı uygulama sunucuları kullanıyoruz veya cache ayrı bir sunucuda tumak istiyorum dedginizde mencache ve rediscache
    //aklınızda tutmak gerek ama bizim uygulama sunucumuz tek oldugundan  bir AES  üzerinden bir yayın yapıyoruz ve ozaman memorycache fazlasıyla yeterli olacaktır.
    public class MemoryCacheManager : ICacheManager
    { 
        //Biz burada microfostun Default memory cache ni kullanacagız o acıdan ben bir adet cache nesnesi oluştururak aşagıyı onunla yönetmek istiyorum ve
        //protected yaparak dışardan erişimi engelemiş oluyorum.
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        public void Add(string key, object data, int cacheTime=60)//default time mızda 60 dk.yani 60  dk cachede duracak.
        {
            //Öncelikle datayı bir kontrol etmem lazım onun için if blogunu kullanarak.
            //Eger bana gönderdigin data null ise yani cache liyecem data yoksa ozaman return olur.
            if (data==null)
            {
                return;
            }
            //Eger data null degilse cache ekliyecegiz datayı
            //AbsoluteExpiration=Burda cahcede ne kadar zaman duracak bilgisini tutar.DateTime diyerek 60 dk  itibaren  cache de tut.
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            //Bir CacheItem eklememiz gerekir.cacheitem bizden bir key istiyor key yukarıdan geliyor.sonra data istiyor ve en son policy(kuralı) ver bana diyor.
            Cache.Add(new CacheItem(key, data), policy);
        }

        public void Clear()
        {
            //cachedeki bütün itemlerı,itemin key degerini kullanıp yani tüm cache datalarını siliyoruz.
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }

        public T Get<T>(string key)
        {
            //Mevcut key nesnemde yada cache datalarımdan bana sana gönderdigim key isminde olan cache döndür.cache datsasını çek T ye döndür bana ver.
            return (T)Cache[key];//Cache datasını çek onu T ye döndür ve bana ver.
        }

        public bool IsAdd(string key)
        {
            //Benim gönderdigim Key  data Cache de varmı 
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //Gönderdigim pattern RegexOptions singleline yaparak tek satırda gönderecegiz.
            //compiled ediyoruz en sonda IngnoreCase kurallarını veriyorum.
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //Hangi cacheleri silecegiz onun içinde keysToRemove diye bir parametre tanımlıyoruz sonra aşagıdaki koddaki gibi işlemleri gercekleştiriyoruz.
            var keysToRemove = Cache.Where(d => regex.IsMatch(d.Key)).Select(d => d.Key).ToList();
            //Bellekten silmek için sectigim dataları forech döngüsünü kullarak işlemi gercekleştiririz.
            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }
    }
}
