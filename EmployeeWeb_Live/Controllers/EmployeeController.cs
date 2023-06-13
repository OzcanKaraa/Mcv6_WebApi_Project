using EmployeeWeb_Live.Models;
using EmployeeWeb_Live.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace EmployeeWeb_Live.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;  
        }

        public async Task<IActionResult> Index()
        {
            var employees= await _service.GetAll();

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind ("FName,LName,City")] Employee employee)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7100/EmployeeEF");

                // Oluşturulan nesneyi JSON formatına çevirme işlemi gerekmekte.Bunu yapmak için de JSonSerializer ile Serialize metodunu kullanarak yapıyoruz.

                var serializeEmployee = JsonSerializer.Serialize(employee);

                StringContent stringContent=new StringContent(serializeEmployee, Encoding.UTF8,"application/json");

                var postResult = client.PostAsync("api/EmployeeEF", stringContent).Result;

                if (postResult.IsSuccessStatusCode) 
                {
                    return RedirectToAction("Index");
                }
            }

            return View(employee);

        }
    }
}
