using Microsoft.AspNetCore.Mvc;
using NZWalk.UI.Models.DTOs;

namespace NZWalk.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        string RegionAPI = "https://localhost:7233/api/Region";

        public async Task<IActionResult> Index()
        {
            List<RegionDto> responce = new List<RegionDto>();
            try
            {
                //Get all the resion From API
                var client = httpClientFactory.CreateClient();
                HttpResponseMessage httpResponseMessage = await client.GetAsync(RegionAPI);
                httpResponseMessage.EnsureSuccessStatusCode();
                //var stringResponceBody= await httpResponseMessage.Content.ReadAsStringAsync();

               responce.AddRange( await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
                
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View(responce);
        }
    }
}
