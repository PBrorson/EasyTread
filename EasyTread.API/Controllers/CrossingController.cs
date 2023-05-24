using EasyTread.API.Hubs;
using EasyTread.Database;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;
using EasyTread.Services.Interface.mappers;
using EasyTread.Services.Interface.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EasyTread.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrossingController : ControllerBase
    {
        private readonly ILogger<CrossingController> _logger;
        private readonly ICrossingService _crossingService;
        private readonly ICrossingMapper _crossingMapper;
        private readonly IValidationService _validationService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public CrossingController(ILogger<CrossingController> logger, ICrossingService crossingService, ICrossingMapper crossingMapper, EasyTreadContext context, IValidationService validationService, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _crossingService = crossingService;
            _crossingMapper = crossingMapper;
            _validationService = validationService;
            _hubContext = hubContext;
        }

        [HttpGet("GetAllCrossingVehicles")]
        public async Task<ActionResult<ICollection<CrossingResponse>>> GetAllCrossingVehiclesAsync()
        {
            var crossings = await _crossingService.GetAllCrossingVehiclesAsync();

            return Ok(crossings);
        }

        [HttpGet("GetAllBadAndMarginalVehicles")]
        public async Task<ActionResult<ICollection<CrossingResponse>>> GetAllBadAndMarginalVehiclesAsync()
        {
            var crossings = await _crossingService.GetAllBadAndMarginalVehiclesAsync();

            return Ok(crossings);
        }

        [HttpPost("CreateCrossing")]
        public async Task<ActionResult> CreateCrossingAsync([FromBody] CrossingRequest crossingRequest)
        {
            string dealerNumber = HttpContext.Request.Headers["dealerNumber"];

            if (dealerNumber != null)
            {
                if (!_validationService.IsValidSwedishLicensePlate(crossingRequest.Results.LicensePlate.Plate))
                {
                    ModelState.AddModelError("LicensePlate", "Invalid Swedish license plate format.");
                    _logger.LogWarning("Invalid Swedish license plate format: {LicensePlate}", crossingRequest.Results.LicensePlate.Plate);
                }

                if (ModelState.IsValid)
                {
                    await _crossingService.CreateCrossingAsync(crossingRequest, dealerNumber);

                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", "A new crossing has been created!");

                    return NoContent();
                }
            }
            else
            {
                _logger.LogWarning("Missing dealer header in the request");
            }

            return BadRequest(ModelState);
        }

        [HttpGet("ShowLatestCrossing")]
        public async Task<ActionResult<CrossingResponse>> ShowLatestCrossingAsync()
        {
            try
            {
                var latestCrossing = await _crossingService.ShowLatestCrossingAsync();

                if (latestCrossing == null)
                {
                    return NotFound();
                }

                return Ok(latestCrossing);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ShowANumberOfLatestCrossings")]
        public async Task<ActionResult<List<CrossingResponse>>> ShowANumberOfLatestCrossingsAsync([FromQuery] int count)
        {
            var crossings = await _crossingService.ShowANumberOfLatestCrossingsAsync(count);
            if (crossings == null)
            {
                return NotFound();
            }
            return Ok(crossings);
        }

        [HttpGet("GetCrossingByLicensePlate")]
        public ActionResult<List<CrossingResponse>> GetCrossingsByLicensePlate(string licensePlate)
        {
            var responseList = _crossingService.GetCrossingByLicensePlate(licensePlate);

            if (responseList == null || !responseList.Any())
            {
                return NotFound();
            }

            return Ok(responseList);
        }
    }
}