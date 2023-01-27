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
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _context;


        public OrderController(IUnitOfWork context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index(int page = 1)
        {

            //paging 
            int Skip = (page - 1) * AppConst.TakeCount;

            int totalCount = await _context.OrderManagerUW.GetCountAsync(a => !a.IsDeleted);

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


            var model = await _context.OrderManagerUW.GetWithSkipAsync(a => !a.IsDeleted, a => a.OrderByDescending(a => a.CreateDate), "Tbl_Customer", Skip, AppConst.TakeCount);


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

            var vm = new OrderViewModel
            {
                customers = allcustomers

            };

            return View(vm);

        }

        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    order.Code = AppUtility.GenerateGuidToken(8);

                    if (order.IsComplete)
                    {
                        order.CompleteDateTime = DateTime.Now;
                    }

                    _context.OrderManagerUW.Create(order);
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

            var vm = new OrderViewModel
            {
                customers = allcustomers,
                order = order

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

            var vm = new OrderViewModel
            {
                customers = allcustomers,
                order = await _context.OrderManagerUW.GetByIdAsync(id)

            };


            return View(vm);


        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Order order)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    if (order.IsComplete)
                    {
                        order.CompleteDateTime = DateTime.Now;
                    }

                    _context.OrderManagerUW.Update(order);
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

            var vm = new OrderViewModel
            {
                customers = allcustomers,
                order = order

            };


            return View(vm);
        }






        public async Task<IActionResult> SetOrderDetails(long id)
        {
             ViewBag.orderId = id;

            var vm = new OrderDetailViewModel
            {
                products = await _context.ProductManagerUW.GetManyAsync(a => !a.IsDeleted && a.Quantity != 0),
               
               
            };

            return View(vm);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SetOrderDetails(OrderDetails orderDetails)
        {

            var vm = new OrderDetailViewModel
            {
                products = await _context.ProductManagerUW.GetManyAsync(a => !a.IsDeleted),
                orderDetails = orderDetails
            };


            if (ModelState.IsValid)
            {

                ViewBag.orderId = orderDetails.FkOrderId;

                //update product qty
                var product = await _context.ProductManagerUW.GetByIdAsync(orderDetails.FkProductId);

                if (product != null)
                {

                    if (product.Quantity < orderDetails.Quantity)
                    {
                        ViewBag.message = AppConst.Product_RemainCount_Exceed;
                        ViewBag.type = AppConst.INFO_TYPE;

                        return View(vm);
                    }

                     product.Quantity -= orderDetails.Quantity;
                    _context.ProductManagerUW.Update(product);

                    await _context.saveAsync();

                    // create order details
                    _context.OrderDetailsManagerUW.Create(orderDetails);
                    await _context.saveAsync();


                    // update order total price
                    var order = await _context.OrderManagerUW.GetByIdAsync(orderDetails.FkOrderId);
                    if (order != null)
                    {

                        order.TotalPrice += (product.Price * orderDetails.Quantity);

                        _context.OrderManagerUW.Update(order);
                        await _context.saveAsync();

                    }
                }

                ViewBag.message = AppConst.SUCCESS_MSG;
                ViewBag.type = AppConst.SUCCESS_TYPE;
            }
            else
            {
                ViewBag.message = AppConst.VALUE_MSG;
                ViewBag.type = AppConst.INFO_TYPE;
            }


            

            return View(vm);
        }







        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {

            var vm = new OrderDtViewModel
            {
                order = await _context.OrderManagerUW.GetAsync(a=>a.Id == id  , "Tbl_Customer"),
                orderDetails = await _context.OrderDetailsManagerUW.GetAsync(a => a.FkOrderId == id, a => a.OrderByDescending(s => s.CreateDate), "Tbl_Product")
            };


            return View(model: vm);
        }





        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var order = await _context.OrderManagerUW.GetByIdAsync(id);

            return View(model: order);
        }





        [ActionName("Delete")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfimed(long id)
        {

            try
            {

                var order = await _context.OrderManagerUW.GetByIdAsync(id);

                order.IsDeleted = true;


                _context.OrderManagerUW.Update(order);

                await _context.saveAsync();


                // delete order details
                foreach (var item in await _context.OrderDetailsManagerUW.GetManyAsync(a => a.FkOrderId == id))
                {
                    item.IsDeleted = true;
                    _context.OrderDetailsManagerUW.Update(item);
                    await _context.saveAsync();
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





    }
}