using FlightBookingRepository.Interface;
using FlightBookingService.Interface;
using SharedData.Models;

namespace FlightBookingService.Services
{
    public class FlightManagementService : IFlightManagementService
    {
        private readonly IFlightManagementRepository _flightManagementRepository;

        public FlightManagementService(IFlightManagementRepository flightManagementRepository)
        {
            _flightManagementRepository = flightManagementRepository;
        }

        public async Task<string> AddAirlineFlightDetailsAsync(AirlineFlightInputParam airlineFlightInputParam)
        {
            return await _flightManagementRepository.AddAirlineFlightDetailsAsync(airlineFlightInputParam);
        }

        public async Task<string> AddAirlineInventoryDetailsAsync(InventoryModel inventory)
        {
            return await _flightManagementRepository.AddAirlineInventoryDetailsAsync(inventory);
        }

        public async Task<string> BookTicketAsync(int flightId, UserBookingDataModel bookingData)
        {
            return await _flightManagementRepository.BookTicketAsync(flightId, bookingData);
        }

        public async Task<string> CancelBookedTicketAsync(string pnr)
        {
            return await _flightManagementRepository.CancelBookedTicketAsync(pnr);
        }

        public async Task<UserBookingDataModel> GetBookedTicketsDetailsAsync(string pnr)
        {
            return await _flightManagementRepository.GetBookedTicketsDetailsAsync(pnr);
        }

        public async Task<IList<UserBookingDataModel>> GetBookedTicketsHistoryAsync(string emailId)
        {
            return await _flightManagementRepository.GetBookedTicketsHistoryAsync(emailId);
        }

        public async Task<string> LoginAirlineUserAsync(UserModel user)
        {
            return await _flightManagementRepository.LoginAirlineUserAsync(user);
        }

        public async Task<string> RegisterAirlineUserAsync(UserModel user)
        {
            return await _flightManagementRepository.RegisterAirlineUserAsync(user);
        }

        public async Task<IList<FlightModel>> SearchFlightDataAsync(SearchInputParam searchInputParam)
        {
            return await _flightManagementRepository.SearchFlightDataAsync(searchInputParam);
        }
    }
}
