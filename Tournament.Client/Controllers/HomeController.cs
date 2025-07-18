using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using Tournament.Client.Clients;
using Tournament.Client.Models;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

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
            var result2 = await SimpleGetAsync2();

            var result3 = await GetWithRequestMessageAsync();

        //    var result4 = await PostWithRequestMessageAsync();

          //  await PatchWithRequestMessageAsync();
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
            return await httpClient.GetFromJsonAsync<IEnumerable<TournamentDto>>("api/TournamentDetails", new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        private async Task<IEnumerable<TournamentDto>> GetWithRequestMessageAsync()
        {         
            return await tournamentClient.GetAsync<IEnumerable<TournamentDto>>("api/tournamentdetails");
        }
        private async Task<TournamentDto> PostWithRequestMessageAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/tournamentdetails");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
            DateTime StartDate = DateTime.UtcNow;
            var tournamentToCreate = new TournamentDto(StartDate)
            {
             //   Id = 0, // Assuming a default value for Id
                Title = "Vinterturneringen+1",
                StartDate = StartDate
              //  EndDate = DateTime.UtcNow.AddDays(60),
               // Games = null
            };
            var jsonCompany = JsonSerializer.Serialize(tournamentToCreate);
            request.Content = new StringContent(jsonCompany);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(json);
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();
            var tournamentDto = JsonSerializer.Deserialize<TournamentDto>(res, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var location = response.Headers.Location;

            return tournamentDto;
        }

        private async Task PatchWithRequestMessageAsync()
        {
            var patchDocument = new JsonPatchDocument<GameCreateDto>();
            patchDocument.Replace(g => g.Title, "Spring Championship 0 Game 1x");
           

            var serializedPatchDoc = Newtonsoft.Json.JsonConvert.SerializeObject(patchDocument);

            var request = new HttpRequestMessage(HttpMethod.Patch, "api/Games/1");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            request.Content = new StringContent(serializedPatchDoc);

            request.Content.Headers.ContentType = new MediaTypeHeaderValue(json);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
