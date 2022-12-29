using Kudos.Services.Dtos;
using Kudos.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kudos.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KudoController : ControllerBase
    {
        private readonly KudoService _kudoService;
        public KudoController(KudoService kudoService)
        {
            _kudoService = kudoService;
        }

        /// <summary>
        /// Creates a Kudo.
        /// </summary>
        /// <param name="kudo"></param>
        /// <returns>A newly created Kudo</returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///     "isSuccessfull": boolean,
        ///      "error": null,
        ///      "kudoResult":
        ///        {
        ///         "id": int,
        ///         "reason": "Team Player","Ownership Mindset","Technical Guidance",
        ///         "description": "string",
        ///         "exchanged": boolean,
        ///         "senderId": int,
        ///         "receiverId": int
        ///        }
        ///     }  
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created kudo</response>
        /// <response code="404">Employee id is wrong</response>
        [HttpPost]
        public async Task<IActionResult> AddKudo(KudoRequest kudo)
        {
            var result = await _kudoService.AddKudoAsync(kudo);
            return Ok(result);
        }

        /// <summary>
        /// Get all given Kudos.
        /// </summary>
        /// <returns>Get all given kudos by id </returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///       "kudoGivent": [  
        ///        {
        ///         "id": int,
        ///         "reason": "Team Player","Ownership Mindset","Technical Guidance",
        ///         "description": "string",
        ///         "exchanged": boolean,
        ///         "senderId": int,
        ///         "receiverId": int
        ///        }],
        ///         "kudoRreceived": [
        ///        {
        ///         "id": int,
        ///         "reason": "Team Player","Ownership Mindset","Technical Guidance",
        ///         "description": "string",
        ///         "exchanged": boolean,
        ///         "senderId": int,
        ///         "receiverId": int
        ///        }]
        ///     }
        /// </remarks>
        /// <response code="200">Returns given/received kudo by id</response>
        [HttpGet]
        public async Task<IActionResult> FilterKudo([Range(1, 200)] int? senderId, [Range(1, 200)] int? receiverId)
        {
            var result = await _kudoService.FilterKudosAsync(senderId, receiverId);
            return Ok(result);
        }

        /// <summary>
        /// Exchange Kudo.
        /// </summary>
        /// <returns>Exchanged kudo</returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///     "isSuccessfull": boolean,
        ///      "error": null,
        ///      "kudoResult": {
        ///         "id": int,
        ///         "reason": "Team Player","Ownership Mindset","Technical Guidance",
        ///         "description": "string",
        ///         "exchanged": boolean,
        ///         "senderId": int,
        ///         "receiverId": int
        ///        }
        ///     } 
        ///
        /// </remarks>
        /// <response code="200">Returns all received kudo by id</response>
        /// <response code="404">Employee id is wrong</response>
        [HttpPost("exchange")]
        public async Task<IActionResult> ExchangeKudo(KudoRequest kudo)
        {
            var result = await _kudoService.ExchangeKudoAsync(kudo);
            return Ok(result);
        }

        /// <summary>
        /// Total Kudos per month.
        /// </summary>
        /// <returns>Get all kudos by date per month</returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     {
        ///     "isSuccessfull": true,
        ///     "error": null,
        ///       "given": [  
        ///        {
        ///         "id": int,
        ///         "reason": "Team Player","Ownership Mindset","Technical Guidance",
        ///         "description": "string",
        ///         "exchanged": boolean,
        ///         "senderId": int,
        ///         "receiverId": int
        ///        }],
        ///         "kudoRreceived": [
        ///        {
        ///         "id": int,
        ///         "reason": "Team Player","Ownership Mindset","Technical Guidance",
        ///         "description": "string",
        ///         "exchanged": boolean,
        ///         "senderId": int,
        ///         "receiverId": int
        ///        }],
        ///        "total":int
        ///     }
        /// </remarks>
        /// <response code="200">Returns all received/given/totoal kudos report by date per month</response>
        /// <response code="404">Date not fount in database</response>
        [HttpGet("report/")]
        public async Task<IActionResult> GetKudosTotalMonth([Required] DateTime date)
        {
            var result = await _kudoService.GetKudosTotalMonthAsync(date);
            return Ok(result);
        }
    }
}