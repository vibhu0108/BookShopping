using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Ecomm_Offfical.Data;
using Project_Ecomm_Offical.DataAccess.Repository;
using Project_Ecomm_Offical.DataAccess.Repository.IRepository;
using Project_Ecomm_Offical.Models;
using Project_Ecomm_Offical.Utility;
using System.Data;
using System.Xml.Linq;

namespace Project_Ecomm_Offical.Areas.Admin.Controllers
{
    [Area ("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Emoloyee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        
        public UserController(ApplicationDbContext context,IUnitOfWork unitOfWork)
        {
                _context = context;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.Include(au => au.Company).ToList(); //ASP NET USER
            var roles = _context.Roles.ToList(); // Aspnet roles
            var userRoles = _context.UserRoles.ToList(); // AspNet user roles
            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(r => r.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.CompanyId != null)
                {
                    user.Company = new Company()
                    {
                        Name = _unitOfWork.Company.Get(Convert.ToInt32(user.CompanyId)).Name
                    };
                }
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name =""
                    };
                }
            }
            //remove Admin role user
            var adminUser = userList.FirstOrDefault(r => r.Role == SD.Role_Admin);
            userList.Remove(adminUser);
            return Json (new {data = userList});
        }
            
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id) // FromBody tab likte hai jab data submit se aa rha ho 
        {
            bool isLocked = false;
            var userInDb = _unitOfWork.ApplicationUser.FirstOrDefault(u => u.Id == id);
            if(userInDb == null) 
                return Json(new {success = false,message = "Something went wrong"});
            if (userInDb != null && userInDb.LockoutEnd > DateTime.Now)
            { 
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = isLocked == true ?
                "User Successfully Locked" :"UserbSuccessfully UnLock"
            });

        }
        #endregion
    }
}
