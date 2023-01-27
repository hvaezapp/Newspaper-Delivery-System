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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        public ProductController(IUnitOfWork context)
        {
            _context = context;

        }

      







        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.ProductManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.ProductManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "Tbl_Supplier", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new ProductViewModel
            {
                suppliers = await _context.SupplierManagerUW.GetManyAsync(a => !a.IsDeleted)
            };

            return View(vm);

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    product.IsExist = product.Quantity > 0 ? true : false;


                    _context.ProductManagerUW.Create(product);
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


            var vm = new ProductViewModel
            {
                product = product,
                suppliers = await _context.SupplierManagerUW.GetManyAsync(a => !a.IsDeleted)
            };

            return View(vm);


        }



        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {


            var vm = new ProductViewModel
            {
                product = await _context.ProductManagerUW.GetByIdAsync(id),
                suppliers = await _context.SupplierManagerUW.GetManyAsync(a => !a.IsDeleted)
            };


            return View(model: vm);
        }





        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    product.IsExist = product.Quantity > 0 ? true : false;

                    _context.ProductManagerUW.Update(product);
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





            var vm = new ProductViewModel
            {
                product = product,
                suppliers = await _context.SupplierManagerUW.GetManyAsync(a => !a.IsDeleted)
            };


            return View(model: vm);


        }




        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {

            var product = await _context.ProductManagerUW.GetAsync(a => a.Id == id, "Tbl_Supplier");
            return View(model: product);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _context.ProductManagerUW.GetByIdAsync(id);
            return View(model: product);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var product = await _context.ProductManagerUW.GetByIdAsync(id);

                product.IsDeleted = true;

                _context.ProductManagerUW.Update(product);
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
