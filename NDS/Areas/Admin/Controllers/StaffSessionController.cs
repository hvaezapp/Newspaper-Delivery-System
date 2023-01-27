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
    public class StaffSessionController : Controller
    {
        private readonly IUnitOfWork _context;

        public StaffSessionController(IUnitOfWork context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index(int page = 1)
        {
            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.StaffSRManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.StaffSRManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "Tbl_Staff", Skip, AppConst.TakeCount);

            return View(model);

        }




        public async Task<IActionResult> AlreadySession(long staffId)
        {


            var vm = new AlreadySessionViewModel
            {
                staff = await _context.StaffManagerUW.GetByIdAsync(staffId)
            };


            return View(vm);
        }






        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> AlreadySession(StaffSessionReady staffSession)
        {

            if (ModelState.IsValid)
            {


                var sessionExist = await _context.StaffSRManagerUW.GetAsync(a => a.FkStaffId == staffSession.FkStaffId && a.SessionDateTime.Date == staffSession.SessionDateTime.Date);

                if (sessionExist != null)
                {
                    TempData["message"]  = AppConst.Session_Ready_Dublicate_MSG;
                    TempData["type"] = AppConst.INFO_TYPE;


                    return RedirectToAction("Index","Staff");
                }



                _context.StaffSRManagerUW.Create(staffSession);
                await _context.saveAsync();



                TempData["message"] = AppConst.SUCCESS_MSG;
                TempData["type"] = AppConst.SUCCESS_TYPE;
            }
            else
            {
                TempData["message"] = AppConst.VALUE_MSG;
                TempData["type"] = AppConst.INFO_TYPE;
            }


            return RedirectToAction("Index", "Staff");
        }



        


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var staffs = _context.StaffManagerUW.Get(a => !a.IsDeleted, null, "").Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName
            });

            var vm = new CreateStaffSessionViewModel
            {
                staffs = staffs

            };


            return View(vm);
        }



        [HttpPost  , ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffSessionReady staffSession)
        {
            if (ModelState.IsValid)
            {


                var sessionExist = await _context.StaffSRManagerUW.GetAsync(a => a.FkStaffId == staffSession.FkStaffId && a.SessionDateTime.Date == staffSession.SessionDateTime.Date);

                if (sessionExist != null)
                {
                    ViewBag.message = AppConst.Session_Ready_Dublicate_MSG;
                    ViewBag.type = AppConst.INFO_TYPE;


                    return RedirectToAction("Index", "Staff");
                }



                _context.StaffSRManagerUW.Create(staffSession);
                await _context.saveAsync();




                ViewBag.message = AppConst.SUCCESS_MSG;
                ViewBag.type = AppConst.SUCCESS_TYPE;
            }
            else
            {
                ViewBag.message = AppConst.VALUE_MSG;
                ViewBag.type = AppConst.INFO_TYPE;
            }


            var staffs = _context.StaffManagerUW.Get(a => !a.IsDeleted, null, "").Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " +s.LastName 
            });

            var vm = new CreateStaffSessionViewModel
            {
                staffs = staffs,
                staffSession = staffSession
            };

            return View(vm);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        { 

            var staffs = _context.StaffManagerUW.Get(a => !a.IsDeleted, null, "").Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName
            });



            var vm = new CreateStaffSessionViewModel
            {
                staffs = staffs,
                staffSession = await _context.StaffSRManagerUW.GetByIdAsync(id)
            };



            return View(vm);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StaffSessionReady staffSession)
        {
            if (ModelState.IsValid)
            {


                _context.StaffSRManagerUW.Update(staffSession);
                await _context.saveAsync();


                ViewBag.message = AppConst.SUCCESS_MSG;
                ViewBag.type = AppConst.SUCCESS_TYPE;
            }
            else
            {
                ViewBag.message = AppConst.VALUE_MSG;
                ViewBag.type = AppConst.INFO_TYPE;
            }


            var staffs = _context.StaffManagerUW.Get(a => !a.IsDeleted, null, "").Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName
            });

            var vm = new CreateStaffSessionViewModel
            {
                staffs = staffs,
                staffSession =staffSession
            };

            return View(vm);
        }





        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var staffSession = await _context.StaffSRManagerUW.GetByIdAsync(id);
            return View(model: staffSession);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var staffSession = await _context.StaffSRManagerUW.GetByIdAsync(id);
                staffSession.IsDeleted = true;
                _context.StaffSRManagerUW.Update(staffSession);


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
