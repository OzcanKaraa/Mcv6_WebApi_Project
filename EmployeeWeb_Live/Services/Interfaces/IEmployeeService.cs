using EmployeeWeb_Live.Models;

namespace EmployeeWeb_Live.Services.Interfaces
{
    public interface IEmployeeService
    {
        // Db üzerinden tüm kayıtları getirecek olan Task/Metot
        Task<IEnumerable<Employee>> GetAll();
    }
}
