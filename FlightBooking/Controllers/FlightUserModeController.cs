using FlightBookingService.Interface;
using SharedData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Controllers
{    
    public partial class FlightController : ControllerBase
    {        
        [Route("flight/search")]
        [HttpPost]
        public async Task<IList<FlightModel>> SearchFlightDataAsync(SearchInputParam searchInputParam)
        {
            if(searchInputParam == null)
            {
                return new List<FlightModel>();                
            }

            return await _flightManagementService.SearchFlightDataAsync(searchInputParam);

        }

        [Route("flight/booking/{flightId}")]
        [HttpPost]
        public async Task<string> BookTicketAsync(int flightId, UserBookingDataModel bookingData)
        {
            if(bookingData == null) { return "No data found"; }

            return await _flightManagementService.BookTicketAsync(flightId, bookingData);
        }

        [Route("flight/ticket/{pnr}")]
        [HttpGet]
        public async Task<UserBookingDataModel> GetBookedTicketsDetailsAsync(string pnr)
        {
            if(pnr == null)
            {
                return new UserBookingDataModel();
            }

            return await _flightManagementService.GetBookedTicketsDetailsAsync(pnr);
        }

        [Route("flight/booking/history/email/{emailId}")]
        [HttpGet]
        public async Task<IList<UserBookingDataModel>> GetBookedTicketsHistoryAsync(string emailId)
        {
            if(emailId == null) { return new List<UserBookingDataModel>(); }

            return await _flightManagementService.GetBookedTicketsHistoryAsync(emailId);
        }

        [Route("flight/booking/cancel/{pnr}")]
        [HttpGet]
        public async Task<string> CancelBookedTicketAsync(string pnr)
        {
            if (pnr == null)
            {
                return "No data found";
            }

            return await _flightManagementService.CancelBookedTicketAsync(pnr);
        }
    }
}
