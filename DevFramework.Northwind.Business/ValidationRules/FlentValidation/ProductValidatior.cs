using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.ValidationRules.FlentValidation

{
 //Bir teknoloji kullanacaksak klasörleme yönetimini kullanmamız projemiz için daha verimli ve hatasız bir sonuç sunacaktır.
 //Biz fluentValidation teknolojisini projemizde kullanacagız. 
    
//FluentValidation bir veri doğrulama kütüphanesidir. FluentValidation ve benzeri ürünlerin kullanılması, verilerin doğru şekilde
 //yani verilerin oluştururken konulmuş kısıtlamaları sağlayarak kurallara uyumlu halde olmasını ve kullanıcı ya da sistem kaynaklı
 //hataların oluşmasını engeller.

 //Ayrıca FluentValidation kullandığımızda  kuralları bir metot içerisinde belirleyeceğimiz için bu durumun önüne geçmiş oluruz.

 //Validation bir nesnenin(ürünün) formatıyla ilgili işlemlerin
 //uyumlulugunu denetler.örnek olarak ürünün ismini girmek
 //,boş gecmemek ürünün uzunlugunun 10 karakterden uzun olmasın gibi kuralları uygulamada kullanırız.
 

 //Dogrulama teknikleri uygulayacagız.

    //Fluentvalidation Nuget Packet yöneticinden  DevFramework.Northwind.Business yüklüyoruz.

    //Bir class’ın FluentValidation’a dönüşümü için AbstractValidator<T> generic class’ından türemesi gerekmektedir.

    public class ProductValidatior:AbstractValidator<Product>
    {
        //Constructor’da(ctor)  yazarak istediğiniz field için validasyon kuralınızı yazabilirsiniz.
        public ProductValidatior()
        {
            //p.CategoryId NotEmpty() mutlaka boş gecirmemeli demektir.
            RuleFor(p => p.CategoryId).NotEmpty();
            //Kendi mesajımı yazmak istersem şayet RuleFor(p => p.CategoryId).NotEmpty().WithMessage("kendi mesajını yaz");gibi yazarız.

            RuleFor(p => p.ProductName).NotEmpty();

            //UnitPrice 0 dan büyük olmalı GreaterThan(0)
            RuleFor(p => p.UnitPrice).GreaterThan(0);

            RuleFor(p => p.QuantityPerUnit).NotEmpty();

            //p.ProductName minimum 2 karakter ve maximum 20 karakter olmalı.
            RuleFor(p => p.ProductName).Length(2, 20);

            //when(ne zaman) categoryıd==1 eşit oldugu zaman  unitprice minimum 20 den büyük  olmalıdır. 
            RuleFor(p => p.UnitPrice).GreaterThan(20).When(p=>p.CategoryId==1);

            //ProductName A ile başlıyorsa true başlamıyorsa false olacak
            //false oldugunda RuleFor(p => p.ProductName).Must(StartWithA) hata verir.

            //RuleFor(p => p.ProductName).Must(StartWithA);
        }

        //private bool StartWithA(string arg)
        //{
        //    //ture oldugunda bu mestod calısır.
        //    return arg.StartsWith("A");
        //}
    }
}
