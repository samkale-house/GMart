using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GMartUI.Models;
using GMartServiceLibrary.BulkOperationServices;
using GMartServiceLibrary.ProductOperationServices;
using GMartDataLibrary.Repository;
using GMartUtilityLibrary;
using GMartDataLibrary.Models;

namespace GMartUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // IProductService _produtService;
        IUnitOfWork _unitOfWork;
        

        public HomeController(ILogger<HomeController> logger,IProductService productService,IUnitOfWork unitOfWork)
        {
          //  _produtService = productService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /*  public IActionResult Index(string sortOrder,int? pageNumber)
          {
              //   var model = _produtService.getAllProducts();            

              //get from repos
              var productresult = _unitOfWork.productRepository.GetAll();

              //Sorting
              //set viewData
              ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder)? "name_desc":"";
              ViewData["PriceSortParam"] = sortOrder=="price" ? "price_desc" : "price";//sortOrder.equals(price) throws nullre exception                        
              switch(sortOrder)
              {
                  case "name_desc":
                      productresult = productresult.OrderByDescending(p => p.Product_Name);
                      break;
                  case "price_desc":
                      productresult = productresult.OrderByDescending(p => p.Product_Price);
                      break;
                  case "price":
                      productresult = productresult.OrderBy(p => p.Product_Price);
                      break;
                  default:
                      //default sortby productname (a-z) ascs
                      productresult = productresult.OrderBy(p => p.Product_Name);
                      break;
              }

              //Paging
              ViewData["currentSort"] = sortOrder;
              int pageSize = 3;
              int pageindex = pageNumber ?? 1;
              var pagedList = PaginatedGenericList<Product>.Create(productresult,pageindex , pageSize);


              return View(pagedList);//(productresult.ToList());
          }*/
        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            var result = _unitOfWork.productRepository.GetAll("");
            return View(PaginatedGenericList<Product>.Create((IQueryable<Product>)result, pageNumber ?? 1,3));
        }
        

        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
