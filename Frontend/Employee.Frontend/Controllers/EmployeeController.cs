using Employee.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

namespace Employee.Frontend.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory httpClientFactory)=>
            _httpClient = httpClientFactory.CreateClient("EmployeeApiBase");


        //public async IActionResult Index()
        public async Task<IActionResult> Index()
        {
            var data= await GetAllEmployee();
            return View(data);
        }

        public async Task<List<Employees>> GetAllEmployee()
        {
            //var response = await _httpClient.GetAsync("Employee");
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var employees = JsonConvert.DeserializeObject<IEnumerable<Employees>>(content);
            //    return employees;
            //}
            //return new List<Employees>();

            var response = await _httpClient.GetFromJsonAsync<List<Employees>>("Country");
            return response is not null? response : new List<Employees>();
        }
    }
}
