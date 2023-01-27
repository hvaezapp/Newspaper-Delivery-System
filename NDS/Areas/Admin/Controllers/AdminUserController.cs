using NDS.Models.Domain;
using NDS.Models.Services;
using NDS.Models.UnitOfWork;
using NDS.Models.ViewModels;
using NDS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Areas.AdminPanel.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminUserController : Controller
    {

        private readonly IUnitOfWork _context;
        private readonly IDeleteFile _deleteFile;
        private readonly IUploadFile _uploadFile;



        public AdminUserController(IUnitOfWork context , IDeleteFile deleteFile  , IUploadFile uploadFile)
        {
            _context = context;
            _deleteFile = deleteFile;
            _uploadFile = uploadFile;

        }




        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.AdminUserManagerUW.GetCountAsync(a => !a.IsDeleted);

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

            var model = await _context.AdminUserManagerUW.GetWithSkipAsync(a => !a.IsDeleted, a => a.OrderByDescending(a => a.CreateDate), "", Skip, AppConst.TakeCount);


            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminUser adminUser , string imagename)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    adminUser.ImageUrl = imagename;

                    adminUser.Password = HashEncryption.PasswordEncode(adminUser.Password);

                    _context.AdminUserManagerUW.Create(adminUser);
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


            return View(adminUser);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(long id)
        {

            var user = await _context.AdminUserManagerUW.GetByIdAsync(id);
            return View(user);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUser adminUser , string imagename)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    //product image
                    if (!string.IsNullOrEmpty(imagename))
                    {

                        if (!string.IsNullOrEmpty(adminUser.ImageUrl))
                        {
                            _deleteFile.DeleteFileFromHost(adminUser.ImageUrl, "upload\\user\\normalimage\\",
                                                                              "upload\\user\\thumbnailimage\\");
                        }


                        adminUser.ImageUrl = imagename;

                    }




                    _context.AdminUserManagerUW.Update(adminUser);
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


            return View(adminUser);
        }




        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var adminUser = await _context.AdminUserManagerUW.GetByIdAsync(id);

            return View(model: adminUser);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var adminUser = await _context.AdminUserManagerUW.GetByIdAsync(id);


            return View(model: adminUser);
        }



        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var adminUser = await _context.AdminUserManagerUW.GetByIdAsync(id);

                adminUser.IsDeleted = true;

                _context.AdminUserManagerUW.Update(adminUser);

                await _context.saveAsync();


                if (!string.IsNullOrEmpty(adminUser.ImageUrl))
                {

                    _deleteFile.DeleteFileFromHost(adminUser.ImageUrl, "upload\\user\\normalimage\\",
                                                                           "upload\\user\\thumbnailimage\\");
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

            string filename = _uploadFile.SaveImage(files, "upload\\user\\normalimage\\",
                                                             "upload\\user\\thumbnailimage\\");


            return Json(new { status = "success", message = "Image Successfully Added", imagename = filename });

        }

    }
}