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
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _context;


        public SupplierController(IUnitOfWork context)
        {
            _context = context;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.SupplierManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.SupplierManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var model = new SupplierViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted)
            };

            return View(model);

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _context.SupplierManagerUW.Create(supplier);
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
            catch (Exception ex)
            {


                ViewBag.message = AppConst.FAIL_MSG;
                ViewBag.type = AppConst.DANGER_TYPE;
            }


            var vm = new SupplierViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                cities = await _context.CityManagerUW.GetManyAsync(a => !a.IsDeleted && a.FkProvinceId == supplier.FkProvinceId),
                supplier = supplier
            };


            return View(model: vm);


        }



        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {

            var supplier = await _context.SupplierManagerUW.GetByIdAsync(id);

            var model = new SupplierViewModel
            {
                supplier = supplier,
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                cities = await _context.CityManagerUW.GetManyAsync(a => !a.IsDeleted && a.FkProvinceId == supplier.FkProvinceId)

            };

            return View(model: model);
        }





        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Supplier supplier)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    _context.SupplierManagerUW.Update(supplier);
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


            var vm = new SupplierViewModel
            {
                supplier = supplier,
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                cities = await _context.CityManagerUW.GetManyAsync(a => !a.IsDeleted && a.FkProvinceId == supplier.FkProvinceId)

            };

            return View(model: vm);


        }




        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {

            var supplier = await _context.SupplierManagerUW.GetAsync(a => a.Id == id, "Tbl_Province,Tbl_City");
            return View(model: supplier);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var model = await _context.SupplierManagerUW.GetByIdAsync(id);
            return View(model: model);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var supplier = await _context.SupplierManagerUW.GetByIdAsync(id);
                supplier.IsDeleted = true;
                _context.SupplierManagerUW.Update(supplier);


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
