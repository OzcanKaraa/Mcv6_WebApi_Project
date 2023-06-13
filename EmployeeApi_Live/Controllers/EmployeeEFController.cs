using EmployeeApi_Live;
using EmployeeApi_Live.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi_Draft.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEFController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public EmployeeEFController(AppDbContext context)
        {
            _context = context;       
        }

        // 1. Get
        [HttpGet]
        //
        //[Route("GetEmployees")]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        //2. Post
        [HttpPost]
        //[Route("AddEmployee")]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync()); // BadRequest,NotFound gibiler de var
        }

        //3.GetbyId
        [HttpGet("{id}")]
        //[Route("GetEmployeeById")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return BadRequest("Çalışan bulunamadı... !");
            }

            return Ok(employee); // BadRequest,NotFound
        }

        //4.Update
        [HttpPut("{id}")]
        //[Route("UpdateEmployee")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            var dbemployee = await _context.Employees.FindAsync(request.Id);

            if (dbemployee == null)
            {
                return BadRequest("Çalışan bulunamadı... !");
            }

            dbemployee.FName = request.FName;
            dbemployee.LName = request.LName;
            dbemployee.City = request.City;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync()); // BadRequest,NotFound
        }

        //5.Delete by Id
        [HttpDelete("{id}")]
        //[Route("DeleteEmployee")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return BadRequest("Çalışan bulunamadı... !");
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync()); // BadRequest,NotFound
        }
    }

}

