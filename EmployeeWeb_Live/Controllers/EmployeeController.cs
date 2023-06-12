using EmployeeWeb_Live.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
