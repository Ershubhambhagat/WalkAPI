using Microsoft.AspNetCore.Mvc;
using NZWalk.UI.Models;
using NZWalk.UI.Models.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NZWalk.UI.Controllers
{
    public class RegionsController : Controller
    {
        #region ctor
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        #endregion

        string RegionAPI = "https://localhost:7233/api/Region";

        public async Task<IActionResult> Index()
        {
            #region Index Page
            List<RegionDto> responce = new List<RegionDto>();
            try
            {
                //Get all the resion From API
                var client = httpClientFactory.CreateClient();
                HttpResponseMessage httpResponseMessage = await client.GetAsync(RegionAPI);
                httpResponseMessage.EnsureSuccessStatusCode();
                //var stringResponceBody= await httpResponseMessage.Content.ReadAsStringAsync();

                responce.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View(responce);
            #endregion
        }

        [HttpGet]
        public IActionResult AddRegion()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionViewModels model)
        {

            #region AddRegion
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(RegionAPI),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpRequestMessage2 = await client.SendAsync(httpRequestMessage);
            httpRequestMessage2.EnsureSuccessStatusCode();
            var responce = await httpRequestMessage2.Content.ReadFromJsonAsync<RegionDto>();
            if (responce != null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View(); 
            #endregion

        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Id = id;
            var client = httpClientFactory.CreateClient();
            var responce=await client.GetFromJsonAsync<RegionDto>($"https://localhost:7233/api/Region/{id.ToString()}");
            if (responce is not null)
            {

                return View(responce);

            }
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult>Edit(RegionDto regionDto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7233/api/Region/{regionDto.Id}"),
                    Content = new StringContent(JsonSerializer.Serialize(regionDto), Encoding.UTF8, "application/json")
                };
                var httpRequestMessage2 = await client.SendAsync(httpRequestMessage);
              //  httpRequestMessage2.EnsureSuccessStatusCode();
                var responce = await httpRequestMessage2.Content.ReadFromJsonAsync<RegionDto>();

                if (responce != null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto regionDto)
        {
            try
            {

                var client = httpClientFactory.CreateClient();
                var httpRequestMessage2 = await client.DeleteAsync($"https://localhost:7233/api/Region/{regionDto.Id}");

                httpRequestMessage2.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");

            }
            catch (Exception)
            {

                throw;
            }
            return View("Edit");

        }
    }
}