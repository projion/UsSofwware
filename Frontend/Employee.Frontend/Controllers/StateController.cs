using Employee.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

namespace Employee.Frontend.Controllers
{
    public class StateController : Controller
    {
        private readonly HttpClient _httpClient;

        public StateController(IHttpClientFactory httpClientFactory)=>
            _httpClient = httpClientFactory.CreateClient("EmployeeApiBase");


        //public async IActionResult Index()
        public async Task<IActionResult> Index()
        {
            var data= await GetAllState();
            return View(data);
        }

        public async Task<IEnumerable<State>> GetAllState()
        {
            //var response = await _httpClient.GetAsync("State");
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var countries = JsonConvert.DeserializeObject<IEnumerable<State>>(content);
            //    return countries;
            //}
            //return new List<State>();

            var response = await _httpClient.GetFromJsonAsync<List<State>>("Country");
            
            return response is not null ? response : new List<State>();
        }
        public async Task<IActionResult> AddOrEdit(int Id)
        {
            if (Id==0)
            {
                //create From
                return View(new State());
            }
            else
            {
                //Find By Id
                var data = await _httpClient.GetAsync($"State/{Id}");
                if (data.IsSuccessStatusCode)
                {
                    var result = await data.Content.ReadFromJsonAsync<State>();
                    return View(result);
                }
            }
            return View(new State());
        }
        
    }
}
