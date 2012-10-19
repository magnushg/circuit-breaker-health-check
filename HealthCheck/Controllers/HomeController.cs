using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCheck.Services;

namespace HealthCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringService _stringService;
        private readonly IAlwaysFails _alwaysFails;

        public HomeController(IStringService stringService, IAlwaysFails alwaysFails)
        {
            _stringService = stringService;
            _alwaysFails = alwaysFails;
        }

        public ActionResult Index()
        {
            try
            {
                _alwaysFails.Fail();
            }
            catch (Exception)
            {
            }
            
            return View();
        }
    }
}
