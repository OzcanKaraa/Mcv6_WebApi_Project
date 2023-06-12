using EmployeeWeb_Live.Models;
using EmployeeWeb_Live.Services.Interfaces;

namespace EmployeeWeb_Live.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
