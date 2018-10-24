using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace netcore_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private string beerApi = "https://go-for-beer.azurewebsites.net/secured/beers";
        private string amazonBeerApi = "https://go-for-beer.azurewebsites.net/secured/amazon-beers";
        private string carsApi = "https://api-np.group.swissre.com/pccc-train/gw/cars/cars";
        private string userApi = "https://go-for-beer.azurewebsites.net/user";

        // GET api/values
        [Authorize]
        [HttpGet("beer")]
        public async Task<ActionResult<string>> Beer()
        {
            var apiAddress = System.Environment.GetEnvironmentVariable("BEER_API_URL");
            if(string.IsNullOrWhiteSpace(apiAddress))
            {
                apiAddress = beerApi;
            }
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.Headers.Authorization = GetAuthHeader(HttpContext.Request);
                request.RequestUri = new Uri(apiAddress);
                request.Method = HttpMethod.Get;
                try
                {
                    var response = await client.SendAsync(request);
                    if(response.StatusCode == HttpStatusCode.Unauthorized)
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

        // GET api/values
        [Authorize]
        [HttpGet("amazonbeer")]
        public async Task<ActionResult<string>> AmazonBeer()
        {
            var apiAddress = System.Environment.GetEnvironmentVariable("AMAZON_BEER_API_URL");
            if (string.IsNullOrWhiteSpace(apiAddress))
            {
                apiAddress = amazonBeerApi;
            }
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.Headers.Authorization = GetAuthHeader(HttpContext.Request);
                request.RequestUri = new Uri(apiAddress);
                request.Method = HttpMethod.Get;
                try
                {
                    var response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }
                    var message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<string>> User()
        {
            var apiAddress = System.Environment.GetEnvironmentVariable("USER_API_URL");
            if (string.IsNullOrWhiteSpace(apiAddress))
            {
                apiAddress = userApi;
            }
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.Headers.Authorization = GetAuthHeader(HttpContext.Request);
                request.RequestUri = new Uri(apiAddress);
                request.Method = HttpMethod.Get;
                try
                {
                    var response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }
                    var message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }

        [HttpGet("public")]
        public ActionResult<IEnumerable<string>> Public()
        {
            return new string[] { "value1", "value2", "value3" };
        }

        [Authorize]
        [HttpGet("cars")]
        public async Task<ActionResult<string>> Cars()
        {
            var apiAddress = System.Environment.GetEnvironmentVariable("CARS_API_URL");
            if (string.IsNullOrWhiteSpace(apiAddress))
            {
                apiAddress = carsApi;
            }
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.Headers.Authorization = GetAuthHeader(HttpContext.Request);
                request.RequestUri = new Uri(apiAddress);
                request.Method = HttpMethod.Get;
                try
                {
                    var response = await client.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }
                    var message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                catch (Exception ex)
                {
                    return "Cars endpoint error: " + ex.ToString();
                }
            }
        }

        private AuthenticationHeaderValue GetAuthHeader(HttpRequest request)
        {
            var header = request.Headers["Authorization"];
            var headerArr = header[0].Split(new[] { ' ' });
            return new AuthenticationHeaderValue("Bearer", headerArr[1]);
        }

    }
}
