using SharedData.Models;
using FlightBookingService.Interface;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using FlightBooking.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Controllers
{
    /// <summary>
    /// FlightController
    /// </summary>
    /// <response code="401">Unauthorize</response>
    /// <response code="200">Success</response>
    [ApiVersion("1.0")]   
    [ApiController]
    [Route("api/v{version:ApiVersion}/")]
    public partial class FlightController : ControllerBase
    {
        private readonly IFlightManagementService _flightManagementService;
        private readonly IConfiguration _configuration;

        public FlightController(IFlightManagementService flightManagementService, IConfiguration configuration)
        {
            _flightManagementService = flightManagementService;
            _configuration = configuration;
        }

        [Route("flight/airline/register")]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterAirlineUserAsync(UserModel user)
        {
            if(user == null)
            {
                return NotFound(); ;
            }

            return await _flightManagementService.RegisterAirlineUserAsync(user);
        }

        [Route("flight/admin/login")]
        [HttpPost]
        public async Task<ActionResult<ResponseTokenModel>> LoginAirlineUserAsync(UserModel user)
        {
            if (user == null)
            {
                return NotFound();
            }
            
            var result = await _flightManagementService.LoginAirlineUserAsync(user);
            if (result.Contains("User Login successful"))
            {                       
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = JwtAuthentication.GetToken(authClaims, _configuration);

                return new ResponseTokenModel { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo };
            }            

            return Unauthorized();
        }

        [Route("flight/airline-flight/add")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> AddAirlineFlightDetailsAsync(AirlineFlightInputParam airlineFlightInputParam)
        {
            if(airlineFlightInputParam == null)
            {
                return Unauthorized();
            }

            return await _flightManagementService.AddAirlineFlightDetailsAsync(airlineFlightInputParam);
        }


        [Route("flight/airline/inventory/add")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> AddAirlineInventoryDetailsAsync(InventoryModel inventory)
        {
            if (inventory == null)
            {
                return NotFound();
            }

            return await _flightManagementService.AddAirlineInventoryDetailsAsync(inventory);
        }
    }
}
