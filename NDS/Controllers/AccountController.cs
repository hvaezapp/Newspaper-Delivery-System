using NDS.Models.UnitOfWork;
using NDS.Models.ViewModels;
using NDS.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NDS.Controllers
{

    public class AccountController : Controller
    {

        private readonly IUnitOfWork _context;

        public AccountController(IUnitOfWork context)

        {
            _context = context;

        }



        #region Login User

        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                var user = await _context.AdminUserManagerUW.GetAsync(a => !a.IsDeleted && a.UserName == model.UserName.Trim() && a.Password == HashEncryption.PasswordEncode(model.Password.Trim()));

                if (user != null)
                {
                    var claim = new List<Claim>
                    {
                        new Claim("userid",user.Id.ToString()),
                        new Claim("useraccount",user.ImageUrl),
                        new Claim("useremail",user.Email)

                    };

                    var option = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTime.Now.AddMonths(1)

                    };

                    await HttpContext.SignInAsync(new GenericPrincipal(new ClaimsIdentity(claim, "Coockies"), null), option);


                    return Redirect("/Admin/Home");

                }
                else
                {
                   // ViewBag.message = AppConst.USERNAME_PASSWORD_INVALID_MSG;
                 //   ViewBag.type = AppConst.INFO_TYPE;

                    ModelState.AddModelError("Password", AppConst.USERNAME_PASSWORD_INVALID_MSG);
                }

            }
            else
            {
               
                ModelState.AddModelError("Password", AppConst.VALUE_MSG);
            }


            return View(model);

        }


        #endregion





        [HttpGet  , Route("/Signout")]
        public IActionResult Signout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }



    }
}
