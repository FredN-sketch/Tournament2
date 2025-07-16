using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Tournament.Client.Clients;
using Tournament.Client.Models;
using Tournament.Core.Dto;

namespace Tournament.Client.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient httpClient;
        private const string json = "application/json";
        private readonly ITournamentClient tournamentClient;
        public HomeController(IHttpClientFactory httpClientFactory, ITournamentClient tournamentClient)
        {
            //this.tournamentClient = tournamentClient;
            //httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("https://localhost:5001/api/tournament/");
            //httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(json));
            //httpClient.Timeout = new TimeSpan(0, 0, 5);

            httpClient = httpClientFactory.CreateClient("TournamentClient");
            this.tournamentClient = tournamentClient;
        }

        public async Task <IActionResult> Index()
        {
            var result = await SimpleGetAsync();
            //var result2 = await SimpleGetAsync2();

            //var result3 = await GetWithRequestMessageAsync();

            //var result4 = await PostWithRequestMessageAsync();

            //await PatchWithRequestMessageAsync();
            return View();
        }


        private async Task<IEnumerable<TournamentDto>> SimpleGetAsync()
        {
            var response = await httpClient.GetAsync("api/TournamentDetails");
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();

            var tournaments = JsonSerializer.Deserialize<IEnumerable<TournamentDto>>(res, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return tournaments!;
        }
        private async Task<IEnumerable<TournamentDto>> SimpleGetAsync2()
        {
            throw new NotImplementedException();
        }
        private async Task<IEnumerable<TournamentDto>> GetWithRequestMessageAsync()
        {
            throw new NotImplementedException();
        }
        private async Task<IEnumerable<TournamentDto>> PostWithRequestMessageAsync()
        {
            throw new NotImplementedException();
        }

        private async Task PatchWithRequestMessageAsync()
        {
            throw new NotImplementedException();
        }



       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
