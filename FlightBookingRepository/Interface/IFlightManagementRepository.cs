using SharedData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingRepository.Interface
{
    public interface IFlightManagementRepository
    {
        Task<string> RegisterAirlineUserAsync(UserModel user);
        Task<string> LoginAirlineUserAsync(UserModel user);
        Task<string> AddAirlineFlightDetailsAsync(AirlineFlightInputParam airlineFlightInputParam);
        Task<string> AddAirlineInventoryDetailsAsync(InventoryModel inventory);
        Task<IList<FlightModel>> SearchFlightDataAsync(SearchInputParam searchInputParam);
        Task<string> BookTicketAsync(int flightId, UserBookingDataModel bookingData);
        Task<UserBookingDataModel> GetBookedTicketsDetailsAsync(string pnr);
        Task<IList<UserBookingDataModel>> GetBookedTicketsHistoryAsync(string emailId);
        Task<string> CancelBookedTicketAsync(string pnr);
    }
}
