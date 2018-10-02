using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace netcore_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private string beerApi = "https://go-for-beer.azurewebsites.net/secured/beers";

        // GET api/values
        [Authorize]
        [HttpGet("protected")]
        public async Task<ActionResult<string>> Protected()
        {
            var header = HttpContext.Request.Headers["Authorization"];
            var headerArr = header[0].Split(new[] { ' ' });
            var apiAddress = System.Environment.GetEnvironmentVariable("REMOTE_API_URL");
            if(string.IsNullOrWhiteSpace(apiAddress))
            {
                apiAddress = beerApi;
            }
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(headerArr[0], headerArr[1]);
                request.RequestUri = new Uri(apiAddress);
                request.Method = HttpMethod.Get;
                try
                {
                    var response = await client.SendAsync(request);
                    if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }
                    var message = await response.Content.ReadAsStringAsync();
                    return message;
                } catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }

        [HttpGet("public")]
        public ActionResult<IEnumerable<string>> Public()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
