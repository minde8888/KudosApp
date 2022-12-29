using AutoFixture.Xunit2;
using AutoMapper;
using FluentValidation;
using Kudos.Domain.Entities;
using Kudos.Domain.Exceptions;
using Kudos.Domain.Interfaces;
using Kudos.Services.Dtos;
using Kudos.Services.Services;
using Kudos.Services.Services.MapperProfile;
using Moq;


namespace Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _employeeService;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly InlineValidator<EmployeeRequest> _employeeValidator;
        private readonly IMapper _mapper;

        public EmployeeServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _employeeValidator = new InlineValidator<EmployeeRequest>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();

            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _employeeValidator, _mapper);
        }

        [Fact]
        public void EmployeeAdd_DidNotGetData_EmployeeNotFoundException()
        {
            //result 
            Assert.ThrowsAsync<EmployeeNotFoundException>(async () => await _employeeService.AddNewEmployeeAsync(null));
        }

        [Fact]
        public void GetAllEmployee_DidNotGetData_EmployeeNotFoundException()
        {
            //result 
            Assert.ThrowsAsync<EmployeeNotFoundException>(async () => await _employeeService.GetAllEmployeeAsync());
        }

        [Theory, AutoData]
        public async Task AddNewEmployeeAsync_GivenEmployeeObject_ReturnsResult(Employee employee)
        {
            // arrange
            _employeeValidator.RuleFor(x => x.Name).Must(Name => true);
            _employeeRepositoryMock.Setup(x => x.Add(It.IsAny<Employee>())).ReturnsAsync(employee);

            //act
            var employeeResponse = _mapper.Map<EmployeeRequest>(employee);
            var result = await _employeeService.AddNewEmployeeAsync(employeeResponse);

            //result
            Assert.NotNull(result);
            Assert.Equal(result.Employee, employee);
        }

        [Theory, AutoData]
        public async Task GetAllEmployeeAsync_ReturnsResult(Employee employee)
        {
            // arrange
            var listEmployees = new List<Employee>
            {
                employee
            };
            _employeeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(listEmployees);

            //act
            var result = await _employeeService.GetAllEmployeeAsync();

            //result
            Assert.NotNull(result);
            Assert.Equal(result[0], employee);
        }
    }
}