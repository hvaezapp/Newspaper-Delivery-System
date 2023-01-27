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
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _context;


        public CustomerController(IUnitOfWork context)
        {
            _context = context;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.CustomerManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.CustomerManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var model = new CustomerViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted)
            };

            return View(model);

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    customer.IsDeleted = false;
                    customer.CreateDate = DateTime.Now;


                    _context.CustomerManagerUW.Create(customer);

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


            var vm = new CustomerViewModel
            {
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                cities = await _context.CityManagerUW.GetManyAsync(a => !a.IsDeleted && a.FkProvinceId == customer.FkProvinceId),
                customer = customer
            };


            return View(model: vm);


        }


        [HttpGet]

        public async Task<IActionResult> Edit(long id)
        {

            var customer = await _context.CustomerManagerUW.GetByIdAsync(id);

            var model = new CustomerViewModel
            {
                customer = customer,
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                cities = await _context.CityManagerUW.GetManyAsync(a => !a.IsDeleted && a.FkProvinceId == customer.FkProvinceId)

            };

            return View(model: model);
        }





        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    _context.CustomerManagerUW.Update(customer);
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


            var vm = new CustomerViewModel
            {
                customer = customer,
                provinces = await _context.ProvinceManagerUW.GetManyAsync(a => !a.IsDeleted),
                cities = await _context.CityManagerUW.GetManyAsync(a => !a.IsDeleted && a.FkProvinceId == customer.FkProvinceId)

            };

            return View(model: vm);


        }




        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {

            var customer = await _context.CustomerManagerUW.GetAsync(a => a.Id == id, "Tbl_Province,Tbl_City");
            return View(model: customer);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var model = await _context.CustomerManagerUW.GetByIdAsync(id);
            return View(model: model);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var customer = await _context.CustomerManagerUW.GetByIdAsync(id);
                customer.IsDeleted = true;
                _context.CustomerManagerUW.Update(customer);


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
