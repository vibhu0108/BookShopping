using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Ecomm_Offfical.Models;
using Project_Ecomm_Offical.DataAccess.Repository;
using Project_Ecomm_Offical.DataAccess.Repository.IRepository;
using Project_Ecomm_Offical.Models;
using Project_Ecomm_Offical.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace Project_Ecomm_Offical.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");

            // Session 
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim !=null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(sp => sp.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount,count); // SetInt32 - for integer value ,, For string value - string ,, For object - object...
            }                    
            return View (productList);
        }
        public IActionResult Details(int id)
        {
            var productInDb = _unitOfWork.Product.FirstOrDefault(p => p.Id == id, includeProperties: "Category,CoverType");
            if (productInDb == null) return NotFound();
            var shoppingCart = new ShoppingCart()
            {
                Product = productInDb,
                ProductId = productInDb.Id
            };
            //session 
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(sp => sp.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count); // SetInt32 - for integer value ,, For string value - string ,, For object - object...
            }
            return View(shoppingCart);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return NotFound();
            shoppingCart.ApplicationUserId = claim.Value;

            var shoppingCartFromDb = _unitOfWork.ShoppingCart.FirstOrDefault
                (sc => sc.ApplicationUserId == claim.Value && sc.ProductId == shoppingCart.ProductId);

            if (shoppingCartFromDb == null)
                //Add
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            else
                //Update
                shoppingCartFromDb.Count += shoppingCart.Count;
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        //{
        //    shoppingCart.Id = 0;
        //    if (ModelState.IsValid)
        //    {
        //        var claimsIdentity = (ClaimsIdentity)User.Identity;
        //        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //        shoppingCart.ApplicationUserId = claim.Value;

        //        var shoppingCardFromDb = _unitOfWork.ShoppingCart.FirstOrDefault
        //            (sp => sp.ApplicationUserId == claim.Value && sp.ProductId ==shoppingCart.ProductId);
        //        if (shoppingCardFromDb == null)
        //        {
        //            //add
        //            _unitOfWork.ShoppingCart.Add(shoppingCart); 
        //        }
        //        else
        //        {
        //            //update
        //            shoppingCardFromDb.Count += shoppingCart.Count;
        //        }
        //        _unitOfWork.Save();                
        //        // Session
        //        var Count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
        //        HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, Count);
        //        return RedirectToAction(nameof(Index));

        //    }
        //    else
        //    {
        //        var productInDb = _unitOfWork.Product.FirstOrDefault
        //            (p => p.Id == shoppingCart.ProductId, includeProperties: "Category,CoverType");
        //        if (productInDb == null) return NotFound();
        //        var shoppingCartEdit = new ShoppingCart()
        //        {
        //            Product = productInDb,
        //            ProductId = productInDb.Id,
        //        };
        //        return View (shoppingCartEdit);
        //    }
        //}

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