using EmployeeWeb_Live.Models;
using EmployeeWeb_Live.Services.Interfaces;
using EmployeeWeb_Live.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;


namespace EmployeeWeb_Live.Services
{
    public class EmployeeService : IEmployeeService
    {
        // MVC uygulaması burada bir client görevi görecek. O yüzden gerekli kütüphaneleri kullanacak.
        private readonly HttpClient _client;
        private readonly string _ApiBase;


        //public const string BasePath = "https://localhost:7100/api/";

        public EmployeeService(HttpClient client,IConfiguration configuration)
        {
            _client = client;
            _ApiBase = configuration["APISection:BaseAddress"]; // appsettings.json dan geliyor
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            string ApiPath = _ApiBase + "api/EmployeeEF";

            var response= await _client.GetAsync(ApiPath);


            return await response.ReadContentAsync<List<Employee>>();
        }

        public async Task<Employee> GetById(int id)
        {
            string ApiPath = _ApiBase + "api/EmployeeEF/" + id;

            var response = await _client.GetAsync(ApiPath);

            return await response.ReadContentAsync<Employee>();

        }
    }
}
