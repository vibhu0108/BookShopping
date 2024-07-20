using Microsoft.AspNetCore.Mvc;
using Project_Ecomm_Offical.DataAccess.Repository.IRepository;
using Project_Ecomm_Offical.Models;
using Project_Ecomm_Offical.Utility;
using System.Security.Claims;

namespace Project_Ecomm_Offical.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        public IActionResult GetAll()
        {
            var order = _unitOfWork.OrderHeader.GetAll();
            return Json(new { data = order });
        }

        #endregion
        public IActionResult Product()
        {
            List<Product> products = new List<Product>();
            products = _unitOfWork.Product.GetAll().ToList();
            products.Insert(0, new Product { Id = 0, Title = "Select Product Name" });
            ViewBag.products = products;
            var order = _unitOfWork.OrderDetail.GetAll(includeProperties: "OrderHeader,Product");
            return View(order);
        }
        [HttpPost]
        public IActionResult Product(int products)
        {
            List<Product> products1 = new List<Product>();
            products1 = _unitOfWork.Product.GetAll().ToList();
            products1.Insert(0, new Product { Id = 0, Title = "Select Product Name" });
            ViewBag.products = products1;
            var orders = _unitOfWork.OrderDetail.GetAll(o => o.ProductId == products &&
                          o.OrderHeader.OrderStatus == SD.OrderStatusPending, includeProperties: "OrderHeader,Product");

            return View(orders);
        }
        //public IActionResult Approved(int id)
        //{
        //    var claimidentity = (ClaimsIdentity)User.Identity;
        //    var claim = claimidentity.FindFirst(ClaimTypes.NameIdentifier);
        //    var user = _unitOfWork.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Email Empty");
        //    }
        //    else
        //    {

        //    }



        //    OrderHeader orderHeader = new OrderHeader();
        //    orderHeader = _unitOfWork.OrderHeader.Get(id);

        //    orderHeader.OrderStatus = SD.OrderStatusApproved;
        //    _unitOfWork.Save();
        //    return RedirectToAction(nameof(Product));
        //}
        public async Task<IActionResult> OrderCancel( int id)
        {
            var claimIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email Empty");
            }
            else
            {
                //Email
                

            }
            OrderHeader order = (OrderHeader) _unitOfWork.OrderHeader.Get(id);
            order.OrderStatus = SD.OrderStatusCancelled;
            _unitOfWork.Save();            
            return RedirectToAction(nameof(Product));
        }

      
    }
}
