using Abp.AspNetCore.Mvc.Controllers;
using FireProtectionV1.BigScreen.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireProtectionV1.Web.Controllers
{
    public class BigScreenController : AbpController
    {
        IBigScreenManager _manager;

        public BigScreenController(IBigScreenManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 首页：飞线层
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFlyLine()
        {
            return null;// Content(_manager.GetFlyLine());
        }
    }
}
