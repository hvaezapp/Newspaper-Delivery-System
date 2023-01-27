using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDS.Models.Domain;
using NDS.Models.UnitOfWork;
using NDS.Models.ViewModels;
using NDS.Utility;
using System;
using System.Threading.Tasks;

namespace NDS.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class ProvinceController : Controller
    {
        private readonly IUnitOfWork _context;


        public ProvinceController(IUnitOfWork context)
        {
            _context = context;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.ProvinceManagerUW.GetCountAsync(a => !a.IsDeleted);

            ViewBag.PageID = page;

            double remain = totalCount % AppConst.TakeCount;

            if (remain == 0)
            {
                ViewBag.PageCount = totalCount / AppConst.TakeCount;
            }
            else
            {
                ViewBag.PageCount = (totalCount / AppConst.TakeCount) + 1;
            }


            var model = await _context.ProvinceManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           

            return View();

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Province province)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    province.IsDeleted = false;


                    _context.ProvinceManagerUW.Create(province);

                    await _context.saveAsync();



                    ViewBag.message = AppConst.SUCCESS_MSG;
                    ViewBag.type = AppConst.SUCCESS_TYPE;
                }
                else
                {
                    ViewBag.message = AppConst.VALUE_MSG;
                    ViewBag.type = AppConst.INFO_TYPE;
                }
            }
            catch (Exception)
            {


                ViewBag.message = AppConst.FAIL_MSG;
                ViewBag.type = AppConst.DANGER_TYPE;
            }


            return View(province);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {


            var model = await _context.ProvinceManagerUW.GetByIdAsync(id);

            return View(model: model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Province province)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    _context.ProvinceManagerUW.Update(province);
                    await _context.saveAsync();

                    ViewBag.message = AppConst.SUCCESS_MSG;
                    ViewBag.type = AppConst.SUCCESS_TYPE;
                }
                else
                {

                    ViewBag.message = AppConst.VALUE_MSG;
                    ViewBag.type = AppConst.INFO_TYPE;
                }
            }
            catch (Exception)
            {

                ViewBag.message = AppConst.FAIL_MSG;
                ViewBag.type = AppConst.DANGER_TYPE;
            }



            return View(province);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.ProvinceManagerUW.GetByIdAsync(id);

            return View(model: model);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(int id)
        {

            try
            {

                var province = await _context.ProvinceManagerUW.GetByIdAsync(id);

                province.IsDeleted = true;

                _context.ProvinceManagerUW.Update(province);

                await _context.saveAsync();


                TempData["message"] = AppConst.SUCCESS_MSG;
                TempData["type"] = AppConst.SUCCESS_TYPE;

            }
            catch (Exception)
            {


                TempData["message"] = AppConst.FAIL_MSG;
                TempData["type"] = AppConst.DANGER_TYPE;
            }

            return RedirectToAction(nameof(Index));
        }





    }
}
