﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Entities.ComplexTypes
{
    //Listede productId,ProductName,CategoryName isimleriyle başlamasını sağlar.
   public class ProductDetail
    {
        public virtual  int ProductId { get; set; }

        public virtual string ProductName { get; set; }

        public virtual string CategoryName { get; set; }
       


    }
}
