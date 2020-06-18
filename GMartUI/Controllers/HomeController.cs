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
using Microsoft.AspNetCore.Hosting;
using GMartUI.AttributeHelper;

namespace GMartUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // IProductService _produtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger,IProductService productService,IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
          //  _produtService = productService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

         /*public IActionResult Index(string sortOrder,int? pageNumber,string searchStr)
          {              

              //get from repos
              var productresult = _unitOfWork.productRepository.GetAll();

            //searching 
            if (!String.IsNullOrEmpty(searchStr))
            {
                productresult = productresult.Where(product => product.Product_Name.Contains(searchStr));
            }
            ViewData["SearchParam"] = searchStr;

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



        
        //method works best for big data 
        //[LogInfo] :Added globally
        public IActionResult Index(string sortOrder, int? pageNumber,string searchStr)
        {
            ViewData["SearchParam"] = searchStr;
            ViewData["currentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";//default sort:name asc
            ViewData["PriceSortParam"] = sortOrder == "price" ? "price_desc" : "price";//sortOrder.equals(price) throws nullre exception                        
            ViewData["CategorySortParam"] = sortOrder == "category" ? "category_desc" : "category";



            //search and sort,include producttype dataset to load and get producttypename
            var result = _unitOfWork.productRepository.GetAll("ProductType", 
                                                                product => product.Product_Name.Contains(searchStr) || searchStr == null,//where condition expression
                                                                (IQueryable<Product> result) => {                                          //result(result of where query) input coming from getallmethod
                                                                                                 switch (sortOrder)
                                                                                                {
                                                                                                   case "name_desc":
                                                                                                   return result.OrderByDescending(p => p.Product_Name);                                                                                                   
                                                                                                   case "price_desc":
                                                                                                   return result.OrderByDescending(p => p.Product_Price);
                                                                                                   case "price":
                                                                                                   return result.OrderBy(p => p.Product_Price);
                                                                                                   case "category_desc":
                                                                                                   return result.OrderByDescending(p => p.ProductType.Type_Name);
                                                                                                   case "category":
                                                                                                   return result.OrderBy(p => p.ProductType.Type_Name);
                                                                                                   default:
                                                                                                   //default sortby productname (a-z) ascs
                                                                                                    return result.OrderBy(p => p.Product_Name);
                                                                                                }
                                                                                               }

                                                            ) ;

            //paging
            var pagedlist = PaginatedGenericList<Product>.Create(result.AsQueryable<Product>(), pageNumber ?? 1, 3);//This creates a list from IQueryable

            return View(pagedlist);
        }  
        public IActionResult Details(int id)
        {
            if (id!= null || id!=0)
            {
                //var foundProduct = _unitOfWork.productRepository.GetbyId(id);
                var foundProduct = _unitOfWork.productRepository.GetFirstorDefault("ProductType", x => x.ID == id);
                ProductDetailsVM model = new ProductDetailsVM();
                model.Product_Name = foundProduct.Product_Name;
                model.Product_Price = foundProduct.Product_Price;
                model.Company = foundProduct.Company;
                model.Product_ImageUrl = foundProduct.Product_Image;
                //set image full path
                model.ImageFullPath = FileHelper.GetImageFullPathForImage(_webHostEnvironment.WebRootPath, foundProduct.ProductType.Type_Name,foundProduct.Product_Image);
                model.Product_Type = foundProduct.ProductType.Type_Name;
                model.Id = id;
                return View(model);
            }
            return View();//not found view
        }

        public IActionResult Upsert(int? id)
        {
            TempData["ID"] = id;
            var types = _unitOfWork.productTypeRepo.GetAll().ToList();
            TempData["ProductTypes"] = types;
            
            if (id!=null && id!=0)
            {
                var foundProduct = _unitOfWork.productRepository.GetFirstorDefault("ProductType", x => x.ID == id);
                CreateEditProductVm model = new CreateEditProductVm();
                model.Product_Name = foundProduct.Product_Name;
                model.Product_Price = foundProduct.Product_Price;
                model.Company = foundProduct.Company;                
                model.ImageFullPath = FileHelper.GetImageFullPathForImage(_webHostEnvironment.WebRootPath, foundProduct.ProductType.Type_Name, foundProduct.Product_Image);
                model.Product_Type = foundProduct.Product_Type;
                model.ID= (int)id;//nullable to nonnullable
                model.Types =types;
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Upsert(CreateEditProductVm model)
        {
            TempData["ProductTypes"] = TempData["ProductTypes"];
            if (ModelState.IsValid && model!=null)
            {
                Product product = new Product();
                product.Product_Name = model.Product_Name;
                product.Product_Price = model.Product_Price;
                product.Company = model.Company;
                product.Product_Type = model.Product_Type;//use dropdown
                //to do: based on product type save in folder food,toys,etc
                product.Product_Image = FileHelper.GetUniqueFileName(model.Product_Image,_webHostEnvironment.WebRootPath,_unitOfWork.productTypeRepo.GetbyId(model.Product_Type).Type_Name);
                if (model.ID != 0)
                {
                    product.ID = model.ID;
                    _unitOfWork.productRepository.Update(product);
                }
                else 
                { 
                    _unitOfWork.productRepository.Add(product); 
                }
                    
                _unitOfWork.productRepository.SaveDb();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //To do: Add Upsert view and action 
        //To do: Add authorization
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
