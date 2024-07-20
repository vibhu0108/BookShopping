using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Ecomm_Offical.DataAccess.Repository.IRepository;
using Project_Ecomm_Offical.Models;
using Project_Ecomm_Offical.Utility;
using System.Data;

namespace Project_Ecomm_Offical.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
            
        {
                _unitOfWork= unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            //return Json (new {data= _unitOfWork.SP_CALL.List<CoverType>(SD.Proc_GetCoverType) });
            return Json(new { data = _unitOfWork.SP_CALL.List<CoverType>(SD.Proc_GetCoverTypes) });

            //return Json(new {data=_unitOfWork.CoverType.GetAll()});   
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var covertypeInDb   = _unitOfWork.CoverType.Get(id);
            if (covertypeInDb == null)
                return Json(new { success = false, Message = "something went wrong while delete data!! " });
            var param = new DynamicParameters();
            param.Add("@id", id);
            _unitOfWork.SP_CALL.Execute(SD.Proc_DeleteCoverType, param);
            //_unitOfWork.CoverType.Remove(covertypeInDb);
            //_unitOfWork.Save();
            return Json(new { success = true, Message = "data delete successfully" });
        }
        #endregion
        
        public IActionResult Upsert(int? id)
        {
            CoverType coverType= new CoverType();
            if (id==null) return View (coverType);
            var param = new DynamicParameters();
            param.Add("@id", id.GetValueOrDefault());
            coverType =_unitOfWork.SP_CALL.OneRecord<CoverType>(SD.Proc_GetCoverType, param);
            //coverType =_unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType==null) NotFound();
            return View(coverType);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType==null) return BadRequest();
            if (!ModelState.IsValid) return View (coverType);
            var param=new DynamicParameters();
            param.Add("@name", coverType.Name);
            if (coverType.Id == 0)
                _unitOfWork.SP_CALL.Execute(SD.Proc_CreateCoverType, param);
                //_unitOfWork.CoverType.Add(coverType);
            else
            {
                param.Add("@id", coverType.Id);
                _unitOfWork.SP_CALL.Execute(SD.Proc_UpdateCoverType, param);
            }
                //_unitOfWork.CoverType.Update(coverType);
                //_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
