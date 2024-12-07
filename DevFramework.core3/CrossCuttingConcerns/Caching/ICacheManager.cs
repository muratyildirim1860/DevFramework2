 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Caching
{
  public  interface ICacheManager
    {
        //Biz outputcache yapacagımız için hangi data hangi parametrelerle cagırılmışsa ona göre bizim cache oluşturmamız lazım.
        //onun için bizim her cache datasına isim vermemiz lazım cahce datasına şu şekilde  Unikey olusturarak yaparız.

        //Ben sana bir key verecem o keye göre bana  cachedatasını getir.T generic yapıyorum çünkü çalışacagım tip  belli degil.
        T Get<T>(string key);
        //object data ekliyip birde istersem cacheTime gibi yani cache data ne kadar kalacak gibi cachetime uygulayabiliyorum
        void Add(string key, object data, int cacheTime);
        //IsAdd daha önce eklenmişmi böyle bir cachedatası varmı diye kontrollere ihtiyacım olacak.
        bool IsAdd(string key);
        //Cache den data silmemiz gerecek onun içinde Remove yu uyguluyacagız.
        void Remove(string key);
        //Bazen ben bir regularexpresio göre silmek istiyebilirim onun içinde RemoveByPattern kullanacagım.
        void RemoveByPattern(string pattern);
        //Bazende cache tamamen silmek isteyebilirim onun içinde Clear uyguluyacam.
        void Clear();
    }
}
