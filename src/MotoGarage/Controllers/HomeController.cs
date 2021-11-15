using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoGarage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        #region User and roles
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        public IActionResult GrantRole()
        {
            return View();
        }
        public IActionResult RemoveRole()
        {
            return View();
        }
        #endregion

        #region Requests
        public IActionResult CreateServiceRequest()
        {
            return View();
        }
        public IActionResult ChangeStatusForServiceRequest()
        {
            return View();
        }
        public IActionResult SetAssigneeForServiceRequest()
        {
            return View();
        }
        #endregion
    }
}
