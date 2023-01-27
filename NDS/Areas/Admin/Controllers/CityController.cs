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
    public class CityController : Controller
    {
        private readonly IUnitOfWork _context;


        public CityController(IUnitOfWork context)
        {
            _context = context;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.CityManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.CityManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "Tbl_Province", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CityViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted)
            };


            return View(model);

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    city.IsDeleted = false;


                    _context.CityManagerUW.Create(city);

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

            var model = new CityViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                city = city
            };


            return View(model);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var model = new CityViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                city = await _context.CityManagerUW.GetByIdAsync(id)
            };

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(City city)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.CityManagerUW.Update(city);
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


            var model = new CityViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                city = city
            };


            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.CityManagerUW.GetByIdAsync(id);


            return View(model: model);
        }





        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(int id)
        {

            try
            {

                var city = await _context.CityManagerUW.GetByIdAsync(id);

                city.IsDeleted = true;

                _context.CityManagerUW.Update(city);

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




        [HttpPost]
        public async Task<IActionResult> GetCityByProvince(int provinceId, string name)
        {


            TempData["name"] = name + ".FkCityId";
            TempData["id"] = name + "_FkCityId";

            var model = await _context.CityManagerUW.GetManyAsync(a => !a.IsDeleted && a.FkProvinceId == provinceId);


            return View("_GetCity", model);
        }



    }
}
