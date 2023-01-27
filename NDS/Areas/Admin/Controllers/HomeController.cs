using FMIS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDS.Models.UnitOfWork;
using NDS.Models.ViewModels;
using NDS.Utility;
using System;
using System.Threading.Tasks;

namespace NDS.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _context;

        public HomeController(IUnitOfWork context)
        {
            _context = context;


        }

        public async Task<IActionResult> Index()
        {

            var vm = new ReportViewModel
            {
                customerCount = await _context.CustomerManagerUW.GetCountAsync(a=> !a.IsDeleted),
                OrderCount = await  _context.OrderManagerUW.GetCountAsync(a=>!a.IsDeleted),
                productCount = await _context.ProductManagerUW.GetCountAsync(a => !a.IsDeleted),
                supplierCount = await _context.SupplierManagerUW.GetCountAsync(a => !a.IsDeleted)
                
            };



            var lastMonthDate = DateTime.Now.AddMonths(-1).Date;

            var divDay = (DateTime.Now.Date - lastMonthDate).Days;

            string lables = "", data = "";

            for (int i = 1; i <= divDay; i++)
            {
                var currentDate = lastMonthDate.AddDays(i);

                var count = await _context.OrderManagerUW.GetCountAsync(a => !a.IsDeleted && a.CreateDate.Date == currentDate);


                lables += currentDate.Date.ToShortDateString() + (i != divDay ? "," : "");
                data += count + (i != divDay ? "," : "");

               
            }
            ViewBag.lables = lables;
            ViewBag.data = data;


            //................

            lables = data = "";

            foreach (var city in await _context.CityManagerUW.GetManyAsync(a=> !a.IsDeleted))
            {
                var count = await _context.CustomerManagerUW.GetCountAsync(a => !a.IsDeleted && a.FkCityId == city.Id);


                lables += city.Title  + ",";
                data += count + ",";

            }

            ViewBag.citylables = lables;
            ViewBag.citydata = data;



            //...............

            ViewBag.deliveredCount = await _context.PublishPlanManagerUW.GetCountAsync(a => !a.IsDeleted && a.Status == PublishPlanStatus.Delivered.ToInt()); ;
            ViewBag.deliveringCount = await _context.PublishPlanManagerUW.GetCountAsync(a => !a.IsDeleted && a.Status == PublishPlanStatus.Delivering.ToInt()); ;
            ViewBag.notDeliverCount = await _context.PublishPlanManagerUW.GetCountAsync(a=>!a.IsDeleted && a.Status == PublishPlanStatus.NotDelivered.ToInt());


            return View(vm);
        }
    }
}
