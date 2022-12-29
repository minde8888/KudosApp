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
    public class KudoServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IKudoRepository> _kudoRepositoryMock;
        private readonly InlineValidator<DateTime> _dateTimeValidator;
        private readonly InlineValidator<KudoRequest> _kudoValidator;

        private readonly KudoService _kudoService;

        public KudoServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _dateTimeValidator = new InlineValidator<DateTime>();
            _kudoValidator = new InlineValidator<KudoRequest>();
            _kudoRepositoryMock = new Mock<IKudoRepository>();

            _kudoService = new KudoService(_kudoRepositoryMock.Object, _mapper, _dateTimeValidator, _kudoValidator);
        }

        [Fact]
        public void AddKudoAsync_DidNotGetData_KudoNotFoundException()
        {
            //result 
            Assert.ThrowsAsync<KudoNotAllowedException>(async () => await _kudoService.AddKudoAsync(null));
        }

        [Theory, AutoData]
        public void AddKudoAsync_DidNotGetReason_KudoReasonNotFoundException(KudoRequest kudo)
        {
            //result 
            Assert.ThrowsAsync<KudoReasonDescriptionNotFoundException>(async () => await _kudoService.AddKudoAsync(kudo));
        }

        [Fact]
        public void ExchangeKudoById_DidNotGetData_KudoExistException()
        {
            //result 
            Assert.ThrowsAsync<KudoExistException>(async () => await _kudoService.ExchangeKudoAsync(null));
        }

        [Fact]
        public void GetKudosTotalMonthAsync_DidNotGetData_KudoNotFoundException()
        {
            //result 
            Assert.ThrowsAsync<KudoNotAllowedException>(async () => await _kudoService.GetKudosTotalMonthAsync(new DateTime()));
        }

        [Theory, AutoData]
        public async Task AddKudoAsync_GivenDataKudo_ReturnsResult(Kudo kudo)
        {
            // arrange
            _kudoValidator.RuleFor(x => x.Description).Must(Description => true);
            kudo.Reason = "Team Player";
            var kudoSave = _mapper.Map<KudoRequest>(kudo);
            _kudoRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Kudo>())).ReturnsAsync(kudo);

            //act 
            var response = await _kudoService.AddKudoAsync(kudoSave);

            //result
            Assert.Equal(response.KudoResult.Id, kudo.Id);
        }

        [Theory, AutoData]
        public async Task ExchangeKudo_GivenDataKudo_ReturnsResult(Kudo kudo)
        {
            // arrange
            _kudoValidator.RuleFor(x => x.Description).Must(Description => true);
            kudo.Reason = "Team Player";
            var kudoSave = _mapper.Map<KudoRequest>(kudo);
            _kudoRepositoryMock.Setup(x => x.ExchangeAsync(It.IsAny<Kudo>())).ReturnsAsync(kudo);

            //act 
            var response = await _kudoService.ExchangeKudoAsync(kudoSave);

            //result
            Assert.Equal(response.KudoResult.Id, kudo.Id);
        }


        [Theory, AutoData]
        public async Task ExchangeKudo_FilterKudosAsync_ReturnsResult(Kudo kudo, Task<List<Kudo>> list)
        {
            // arrange
            _kudoValidator.RuleFor(x => x.Description).Must(Description => true);
            kudo.Reason = "Team Player";

            var resultKudo = _mapper.Map<List<KudoResult>>(list.Result);

            _kudoRepositoryMock.Setup(x => x.FilterAll(kudo.SenderId, kudo.ReceiverId)).Returns(list);

            //act 
            var response = await _kudoService.FilterKudosAsync(kudo.SenderId, kudo.ReceiverId);

            //result
            Assert.Equal(resultKudo[0].ReceiverId, list.Result[0].ReceiverId);
        }

        [Theory, AutoData]
        public async Task GetKudosTotalMonthAsync_GeTotalKudosPerMonth_ReturnsResult(DateTime minDate, Kudo kudo)
        {
            // arrange
            var maxDate = minDate.AddMonths(1).AddDays(-1);
            var listKudos = new List<Kudo>
            {
                kudo
            };
            _kudoRepositoryMock.Setup(x => x.TotalKudosMonthAsync(minDate, maxDate)).ReturnsAsync(listKudos);

            //act 
            var response = await _kudoService.GetKudosTotalMonthAsync(minDate);

            //result
            Assert.Equal(response.Given[0].Id, kudo.Id);
            Assert.Equal(response.Received[0].Id, kudo.Id);
        }
    }
}
