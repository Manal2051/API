using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestToken.Data;
using TestToken.DTO;
using TestToken.DTO.EmployeeDto;
using TestToken.Models;
using TestToken.Repositories.GenericRepository;
using TestToken.Repositories.Interfaces;

namespace TestToken.Repositories.Services
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return new ResponseDto
            {
                Message = "Employee has been added successfully",
                IsSucceeded = true,
                StatusCode = 201,
                model = _mapper.Map<EmployeeDto>(employee)
            };
        }

        public async Task<ResponseDto> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ResponseDto
                {
                    Message = "Employee not found!",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return new ResponseDto
            {
                Message = "Employee deleted successfully",
                IsSucceeded = true,
                StatusCode = 200
            };
        }



        public async Task<ResponseDto> GetAllEmployees(int pageNumber, int pageSize)
        {
            var query = _context.Employees.AsNoTracking();

            var totalCount = await query.CountAsync();

            var employees = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!employees.Any())
            {
                return new ResponseDto
                {
                    Message = "No employees found!",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return new ResponseDto
            {
                IsSucceeded = true,
                StatusCode = 200,
                model = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = employeeDtos
                }
            };
        }


        public async Task<ResponseDto> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return new ResponseDto
                {
                    Message = "Employee not found!",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }

            return new ResponseDto
            {
                IsSucceeded = true,
                StatusCode = 200,
                model = _mapper.Map<EmployeeDto>(employee)
            };
        }

        public async Task<ResponseDto> GetEmployeeByName(string name)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.FirstName == name || e.LastName == name);

            if (employee == null)
            {
                return new ResponseDto
                {
                    Message = "Employee not found!",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }

            return new ResponseDto
            {
                IsSucceeded = true,
                StatusCode = 200,
                model = _mapper.Map<EmployeeDto>(employee)
            };
        }

        public async Task<ResponseDto> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ResponseDto
                {
                    Message = "Employee not found!",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }

            _mapper.Map(employeeDto, employee); // update fields
            await _context.SaveChangesAsync();

            return new ResponseDto
            {
                Message = "Employee updated successfully",
                IsSucceeded = true,
                StatusCode = 200,
                model = _mapper.Map<EmployeeDto>(employee)
            };
        }
    }
}
