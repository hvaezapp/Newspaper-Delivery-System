using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDS.Models.Domain;
using NDS.Models.UnitOfWork;
using NDS.Utility;
using System;
using System.Threading.Tasks;


namespace NDS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TransController : Controller
    {
        private readonly IUnitOfWork _context;


        public TransController(IUnitOfWork context)
        {
            _context = context;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.TransactionManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.TransactionManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _context.TransactionManagerUW.Create(transaction);
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

            return View(transaction);


        }



        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var model = await _context.TransactionManagerUW.GetByIdAsync(id);
            return View(model);
        }





        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Transaction transaction)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    _context.TransactionManagerUW.Update(transaction);
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


            return View(transaction);


        }




        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {

            var transaction = await _context.TransactionManagerUW.GetByIdAsync(id);
            return View(model: transaction);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var transaction = await _context.TransactionManagerUW.GetByIdAsync(id);
            return View(model: transaction);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var transaction = await _context.TransactionManagerUW.GetByIdAsync(id);

                transaction.IsDeleted = true;
                _context.TransactionManagerUW.Update(transaction);


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
