using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDS.Models.Domain;
using NDS.Models.Services;
using NDS.Models.UnitOfWork;
using NDS.Models.ViewModels;
using NDS.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NDS.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IUploadFile _uploadfile;
        private readonly IDeleteFile _deletefile;


        public StaffController(IUnitOfWork context , IUploadFile uploadFile  , IDeleteFile deleteFile)
        {
            _context = context;
            _uploadfile = uploadFile;
            _deletefile = deleteFile;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.StaffManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.StaffManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "", Skip, AppConst.TakeCount);

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Staff  staff ,string imagename)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    staff.ImageUrl = imagename;

                    _context.StaffManagerUW.Create(staff);
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



            return View(staff);


        }



        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {

            var staff = await _context.StaffManagerUW.GetByIdAsync(id);

          

            return View(model: staff);
        }





        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Staff staff  , string imagename)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    if (!string.IsNullOrEmpty(imagename))
                    {

                        if (!string.IsNullOrEmpty(staff.ImageUrl))
                        {
                            _deletefile.DeleteFileFromHost(staff.ImageUrl, "upload\\staff\\normalimage\\",
                                                                              "upload\\staff\\thumbnailimage\\");
                        }

                        staff.ImageUrl = imagename;
                      
                    }


                    _context.StaffManagerUW.Update(staff);
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


           

            return View(model: staff);


        }




        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {

            var staff = await _context.StaffManagerUW.GetByIdAsync(id);
            return View(model: staff);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var staff = await _context.StaffManagerUW.GetByIdAsync(id);
            return View(model: staff);
        }




        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var staff = await _context.StaffManagerUW.GetByIdAsync(id);

                staff.IsDeleted = true;
                _context.StaffManagerUW.Update(staff);
                await _context.saveAsync();

                if (!string.IsNullOrEmpty(staff.ImageUrl))
                {

                    _deletefile.DeleteFileFromHost(staff.ImageUrl, "upload\\staff\\normalimage\\",
                                                                           "upload\\staff\\thumbnailimage\\");
                }


               


                if (!string.IsNullOrEmpty(staff.ImageUrl))
                {
                    // delete user image
                }


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







        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {

            string filename = _uploadfile.SaveImage(files, "upload\\staff\\normalimage\\",
                                                             "upload\\staff\\thumbnailimage\\");

            return Json(new { status = "success", message = "Image Successfully Added", imagename = filename });

        }




    }
}
