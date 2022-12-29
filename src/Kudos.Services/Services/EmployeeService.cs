using AutoMapper;
using FluentValidation;
using Kudos.Domain.Entities;
using Kudos.Domain.Interfaces;
using Kudos.Services.Dtos;

namespace Kudos.Services.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<EmployeeRequest> _employeeValidator;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, 
            IValidator<EmployeeRequest> employeeValidator,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
            _mapper = mapper;
        }

        public async Task<EmployeeResponse> AddNewEmployeeAsync(EmployeeRequest employee)
        {
            var validationResult = await _employeeValidator.ValidateAsync(employee);
            if (validationResult.IsValid)
            {
                var result = _mapper.Map<Employee>(employee);
                var response = await _employeeRepository.Add(result);

                return new EmployeeResponse
                {
                    Employee = response,
                };
            }

            var errorList = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                errorList.Add(error.ErrorMessage);
            }

            return new EmployeeResponse
            {
                IsSuccessfull = false,
                Error = errorList,
            };
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            var response = await _employeeRepository.GetAll();

            return response;
        }
    }
}
