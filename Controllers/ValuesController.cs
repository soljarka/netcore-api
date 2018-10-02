using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace netcore_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [Authorize]
        [HttpGet("protected")]
        public ActionResult<IEnumerable<string>> Protected()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("public")]
        public ActionResult<IEnumerable<string>> Public()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
