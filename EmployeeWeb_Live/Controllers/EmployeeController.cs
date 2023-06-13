using EmployeeWeb_Live.Models;
using EmployeeWeb_Live.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using EmployeeApi_Live;
using Employee = EmployeeWeb_Live.Models.Employee;

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

        // Post
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

        // Get : Details
        public async Task<IActionResult> Details (int id)
        {
            var employeeDetails = await _service.GetById(id); // var mı yok mu

            if (employeeDetails == null) return View("NotFound");

            return View(employeeDetails);

        }

        // Get : Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var employeeDetails = await _service.GetById(id); // var mı yok mu

            if (employeeDetails == null) return View("NotFound");

            return View(employeeDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FName,LName,City")] Employee employee)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7100/EmployeeEF");

                // Oluşturulan nesneyi JSON formatına çevirme işlemi gerekmekte.Bunu yapmak için de JSonSerializer ile Serialize metodunu kullanarak yapıyoruz.

                var serializeEmployee = JsonSerializer.Serialize(employee);

                StringContent stringContent = new StringContent(serializeEmployee, Encoding.UTF8, "application/json");

                var putResult = client.PutAsync("api/EmployeeEF/" + id, stringContent).Result;

                if (putResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(employee);
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var employeeDetails = await _service.GetById(id); // var mı yok mu

            if (employeeDetails == null) return View("NotFound");

            return View(employeeDetails);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7100/EmployeeEF");

                var deleteResult = client.DeleteAsync("api/EmployeeEF/" + id).Result;

                if (deleteResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}
