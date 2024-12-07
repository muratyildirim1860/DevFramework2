using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.MVC_WEBUI.Models;
using System.Web.Mvc;

namespace DevFramework.Northwind.MVC_WEBUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Product
        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(model);
        }
        public string Add()
        {
            _productService.Add(new Product
            {
                CategoryId = 1,
                ProductName = "Gsm",
                QuantityPerUnit = "1",
                UnitPrice = 21

            });
            return "Added";
        }
        public string AddUpdate()
        {
            _productService.TransactionalOperation(

             new Product
             {
                 CategoryId = 1,
                 ProductName = "computer",
                 QuantityPerUnit = "1",
                 UnitPrice = 23


             }, new Product
             {
                 CategoryId = 1,
                 ProductName = "computer 2",
                 QuantityPerUnit = "1",
                 UnitPrice = 22,
                 ProductId = 2


             });
            return "Done";
        }
    }
}