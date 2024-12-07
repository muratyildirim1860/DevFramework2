using DevFramework.Northwind.Business.ValidationRules.FlentValidation;
using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
   public class ValidationModule:NinjectModule
    {
        public override void Load()
        {
            //Hangi Validation kütüpanesiyele veya hangi validator çalışacagımız hususunda özelikle Client side
            //(istemci tarafı (kullanıcı tarayıcısı)) validation ihiyacımız var.

            //Client side (istemci tarafı (kullanıcı tarayıcısı))validation türünde eğer birisi senden IValidator türünde Product
            //türünde validationa ihtiyac duyarsa ozaman ona ProductValidatior instance(object yani Nesne)’ olarak türetip ver.
            Bind<IValidator<Product>>().To<ProductValidatior>().InSingletonScope();
        }
    }
}
