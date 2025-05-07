using TestToken.DTO;
using TestToken.DTO.EmployeeDto;
using TestToken.Models;
using TestToken.Repositories.GenericRepository;

namespace TestToken.Repositories.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee> 
    {
        Task<ResponseDto> GetAllEmployees(int pageNumber, int pageSize);
        Task<ResponseDto> GetEmployeeById(int id);
        Task<ResponseDto> GetEmployeeByName(string name);
        Task<ResponseDto> AddEmployee(EmployeeDto employee);
        Task<ResponseDto> UpdateEmployee(int id , EmployeeDto employeeDto);
        Task<ResponseDto> DeleteEmployee(int id);
       
    }
}
