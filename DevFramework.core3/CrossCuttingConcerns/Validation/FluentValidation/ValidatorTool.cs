using FluentValidation;
using Microsoft.OpenApi.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Validation.FluentValidation
{
  public class ValidatorTool
    {
        //Oluşturudugumuz FluentValidation sürekli olarak biyerlerden çagırmamiz gerekecek bunun içinde ValidatorTool oluşturuyoruz.
        //Böylece FluentValidation istenildiginde  kullanabilecegiz.

        //validation,cash,crosscutting ,otolizasyon,transejon yönetimi,hata yönetimi,performans yönetimi metodlarını çagrılması süreçleriyle
        //ilgili bu benzer uygulamalar
        //CrossCuttingConcers olarak adlandırılırlar yani uyugulamanın belli yerlerinde uygulamaya ek olarak çagırırız.


        //Gönderdigim validator için entiy(product,category) uygulamadan önce validator yapmasını istiyorum.
        //Birde bir nesneyi validate edecegim için ve hepsinin base de object oldugundan dolayı kullanıyoruz.
        public static void FluentValidate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            //eğer result.Errors.Count>0 bir hata varsa
            if (result.Errors.Count>0)
            {
                throw new ValidationException(result.Errors);
            }
        }


    }
}
