using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDS.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NDS.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class HolidayController : Controller
    {
        public async Task<IActionResult> Index()
        {

            var url = string.Format(@"https://date.nager.at/api/v3/publicholidays/{0}/CA" , DateTime.Now.Year);

            WebClient webClient = new WebClient();
            var data = await webClient.DownloadStringTaskAsync(new Uri(url));
            var model =  JsonConvert.DeserializeObject<List<HolidayViewModel>>(data);

            return View(model);
        }
    }
}
