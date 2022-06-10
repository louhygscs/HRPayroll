using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Api.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("~/")]
        public string Get()
        {
            return "HRM Api";
        }
    }
}