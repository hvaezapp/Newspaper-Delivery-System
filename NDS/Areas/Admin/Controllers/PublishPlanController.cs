using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDS.Models.Domain;
using NDS.Models.UnitOfWork;
using NDS.Models.ViewModels;
using NDS.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class PublishPlanController : Controller
    {
        private readonly IUnitOfWork _context;


        public PublishPlanController(IUnitOfWork context)
        {
            _context = context;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.PublishPlanManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.PublishPlanManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "Tbl_Staff,Tbl_Customer", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {


            var allcustomers = _context.CustomerManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });


            var allStaffs = _context.StaffManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });



            var vm = new PublishPlanViewModel
            {
                customers = allcustomers,
                staffs = allStaffs,
                products =await  _context.ProductManagerUW.GetManyAsync(a=>!a.IsDeleted)
            };

            return View(vm);

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(PublishPlan publishPlan)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    publishPlan.Status = PublishPlanStatus.Delivering.ToInt();

                    _context.PublishPlanManagerUW.Create(publishPlan);
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


            var allcustomers = _context.CustomerManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });


            var allStaffs = _context.CustomerManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });



            var vm = new PublishPlanViewModel
            {
                customers = allcustomers,
                staffs = allStaffs,
                publishPlan = publishPlan,
                products = await _context.ProductManagerUW.GetManyAsync(a => !a.IsDeleted)
            };


            return View(vm);


        }



        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {


            var allcustomers = _context.CustomerManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });


            var allStaffs = _context.StaffManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });


            var vm = new PublishPlanViewModel
            {
                customers = allcustomers,
                staffs = allStaffs,
                publishPlan = await _context.PublishPlanManagerUW.GetByIdAsync(id),
                products = await _context.ProductManagerUW.GetManyAsync(a => !a.IsDeleted)
            };

            



            return View(model: vm);
        }





        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublishPlan publishPlan)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    if (publishPlan.Status == PublishPlanStatus.Delivering.ToInt())
                    {
                        publishPlan.DeliveryDateTime = DateTime.Now;
                    }

                    _context.PublishPlanManagerUW.Update(publishPlan);
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

            var allcustomers = _context.CustomerManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });


            var allStaffs = _context.StaffManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });


            var vm = new PublishPlanViewModel
            {
                customers = allcustomers,
                staffs = allStaffs,
                publishPlan = publishPlan,
                products = await _context.ProductManagerUW.GetManyAsync(a => !a.IsDeleted)
            };


            return View(model: vm);


        }




        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {

            var publishPlan = await _context.PublishPlanManagerUW.GetAsync(a => a.Id == id, "Tbl_Staff,Tbl_Customer,Tbl_Product");
            return View(model: publishPlan);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var publishPlan = await _context.PublishPlanManagerUW.GetByIdAsync(id);
            return View(model: publishPlan);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var publishPlan = await _context.PublishPlanManagerUW.GetByIdAsync(id);
                publishPlan.IsDeleted = true;
                _context.PublishPlanManagerUW.Update(publishPlan);


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
