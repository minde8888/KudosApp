using AutoMapper;
using FluentValidation;
using Kudos.Domain.Entities;
using Kudos.Domain.Exceptions;
using Kudos.Domain.Interfaces;
using Kudos.Services.Dtos;
using Kudos.Services.Validators.Helpers;
using System.ComponentModel;

namespace Kudos.Services.Services
{
    public class KudoService
    {
        private readonly IKudoRepository _kudoRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<DateTime> _dateTimeValidator;
        private readonly IValidator<KudoRequest> _requestValidator;

        public KudoService(IKudoRepository kudoRepository,
            IMapper mapper,
            IValidator<DateTime> dateTimeValidator,
            IValidator<KudoRequest> requestValidator
            )
        {
            _kudoRepository = kudoRepository;
            _mapper = mapper;
            _dateTimeValidator = dateTimeValidator;
            _requestValidator = requestValidator;
        }

        public enum Reason
        {
            [Description("Team Player")]
            TeamPlayer,
            [Description("Ownership Mindset")]
            OwnershipMindset,
            [Description("Technical Guidance")]
            TechnicalGuidance
        }

        public async Task<KudoResponse> AddKudoAsync(KudoRequest kudo)
        {
            var validationResult = await _requestValidator.ValidateAsync(kudo);
            if (validationResult.IsValid)
            {
                var reasonDescription = EnumValidator.GetEnumValueByDescription<Reason>(kudo.Reason);
                kudo.Reason = reasonDescription;

                var kudoSave = _mapper.Map<Kudo>(kudo);
                var response = await _kudoRepository.AddAsync(kudoSave);
                if (response == null) throw new KudoNotAllowedException();
                var kudoResult = _mapper.Map<KudoResult>(response);

                return new KudoResponse
                {
                    IsSuccessfull = true,
                    KudoResult = kudoResult
                };
            }

            var errorList = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                errorList.Add(error.ErrorMessage);
            }

            return new KudoResponse
            {
                IsSuccessfull = false,
                Error = errorList,
            };
        }

        public async Task<List<KudoResult>> FilterKudosAsync(int? senderId, int? receivedId)
        {
            var kudos = _mapper.Map<List<KudoResult>>(await _kudoRepository.FilterAll(senderId, receivedId));

            return kudos;
        }

        public async Task<KudoResponse> ExchangeKudoAsync(KudoRequest kudo)
        {
            var validationResult = await _requestValidator.ValidateAsync(kudo);
            if (validationResult.IsValid)
            {
                var reasonDescription = EnumValidator.GetEnumValueByDescription<Reason>(kudo.Reason);
                kudo.Reason = reasonDescription;

                var kudoToSave = _mapper.Map<Kudo>(kudo);
                var response = await _kudoRepository.ExchangeAsync(kudoToSave);
                if (response == null) throw new KudoNotAllowedException();
                var kudoResult = _mapper.Map<KudoResult>(response);

                return new KudoResponse
                {
                    IsSuccessfull = true,
                    KudoResult = kudoResult
                };
            }

            var errorList = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                errorList.Add(error.ErrorMessage);
            }

            return new KudoResponse
            {
                IsSuccessfull = false,
                Error = errorList,
            };
        }

        public async Task<KudoReport> GetKudosTotalMonthAsync(DateTime minDate)
        {
            var validationResult = await _dateTimeValidator.ValidateAsync(minDate);
            if (validationResult.IsValid)
            {
                var maxDate = minDate.AddMonths(1).AddDays(-1);
                var response = await _kudoRepository.TotalKudosMonthAsync(minDate, maxDate);
                if (response == null) throw new KudoResponseException();

                var result = _mapper.Map<List<KudoResult>>(response);

                return new KudoReport
                {
                    Total = response.Count(),
                    Given = result.FindAll(e => e.SenderId > 0),
                    Received = result.FindAll(e => e.SenderId > 0)
                };
            }

            var errorList = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                errorList.Add(error.ErrorMessage);
            }
            return new KudoReport
            {
                IsSuccessfull = false,
                Error = errorList,
            };
        }
    }
}
