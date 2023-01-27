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
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _context;


        public PaymentController(IUnitOfWork context)
        {
            _context = context;


        }


        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.PaymentManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.PaymentManagerUW.GetWithSkipAsync(a => !a.IsDeleted, null, "Tbl_Customer,Tbl_Order", Skip, AppConst.TakeCount);

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

            var vm = new CreatePaymentViewModel
            {
                customers = allcustomers

            };

            return View(vm);

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(PaymentViewModel payment)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var order = await _context.OrderManagerUW.GetAsync(a => a.Code.Trim() == payment.OrderCode.Trim());

                    if (order != null)
                    {
                        var newpayment = new Payment
                        {
                            Description = payment.Description,
                            FkCustomerId = payment.CustomerId,
                            RefCode = AppUtility.GenerateGuidToken(8),
                            Price = order.TotalPrice,
                            FkOrderId = order.Id

                        };


                        _context.PaymentManagerUW.Create(newpayment);
                        await _context.saveAsync();


                        //update order
                        order.IsComplete = true;
                        order.CompleteDateTime = DateTime.Now;

                        _context.OrderManagerUW.Update(order);
                        await _context.saveAsync();

                        ViewBag.message = AppConst.SUCCESS_MSG;
                        ViewBag.type = AppConst.SUCCESS_TYPE;
                    }
                    else
                    {
                        ViewBag.message = AppConst.OrderCode_Invalid;
                        ViewBag.type = AppConst.INFO_TYPE;

                      
                    }


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


            var vm = new CreatePaymentViewModel
            {
                customers = allcustomers,
                payment = payment

            };



            return View(model: vm);


        }



        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {

            var allcustomers = _context.CustomerManagerUW.Get(a => !a.IsDeleted).Select(s => new NameViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName

            });


            var mainPayment = await _context.PaymentManagerUW.GetAsync(a=>a.Id == id  , "Tbl_Order");

            var vm = new CreatePaymentViewModel
            {
                customers = allcustomers,

                payment = new PaymentViewModel
                {
                    CustomerId = mainPayment.FkCustomerId,
                    OrderId = mainPayment.FkOrderId,
                    Description = mainPayment.Description,
                    OrderCode = mainPayment.Tbl_Order.Code,
                    Price = mainPayment.Price,
                    RefCode = mainPayment.RefCode
                }
            };



            return View(model: vm);
        }





        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentViewModel payment)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var mainPayment = await _context.PaymentManagerUW.GetByIdAsync(payment.OrderId);

                    if (mainPayment != null)
                    {

                        mainPayment.Description = payment.Description;
                        mainPayment.FkCustomerId = payment.CustomerId;
                        mainPayment.FkOrderId = payment.OrderId;
                        mainPayment.Price = payment.Price;
                        mainPayment.RefCode = payment.RefCode;



                        _context.PaymentManagerUW.Update(mainPayment);
                        await _context.saveAsync();



                        ViewBag.message = AppConst.SUCCESS_MSG;
                        ViewBag.type = AppConst.SUCCESS_TYPE;
                    }
                    else
                    {
                        ViewBag.message = AppConst.FAIL_MSG;
                        ViewBag.type = AppConst.INFO_TYPE;
                    }
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


            var vm = new CreatePaymentViewModel
            {
                customers = allcustomers,
                payment = payment

            };

            return View(model: vm);


        }


    }
}

