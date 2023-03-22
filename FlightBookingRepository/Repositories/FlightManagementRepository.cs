using AutoMapper;
using Azure;
using FlightBookingRepository.DataModels;
using FlightBookingRepository.DbContexts;
using FlightBookingRepository.Interface;
using Microsoft.EntityFrameworkCore;
using SharedData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingRepository.Repositories
{
    public class FlightManagementRepository : IFlightManagementRepository
    {
        private readonly FlightManagementContext _flightManagementContext;
        private readonly IMapper _mapper;

        public FlightManagementRepository(FlightManagementContext flightManagementContext, IMapper mapper)
        {
            _flightManagementContext = flightManagementContext;
            _mapper = mapper;
        }
        
        public async Task<string> RegisterAirlineUserAsync(UserModel user)
        {           
            if(user != null)
            {
                var userData = new User();
                userData.Login = user.Login;
                userData.Password = user.Password;

                var isUserAvailable = await this.IsUserAvailableAsync(userData);
                if(!isUserAvailable)
                {
                    _flightManagementContext.Users.Add(userData);
                    await _flightManagementContext.SaveChangesAsync();

                    return "User saved successfully !!";
                } 
                else
                {
                    return $"{user.Login} is already exist.";
                }              
            }

            return "User data is empty";            
        }

        public async Task<string> LoginAirlineUserAsync(UserModel user)
        {            
            if (user != null)
            {
                var userData = new User();
                userData.Login = user.Login;
                userData.Password = user.Password;
                var isUserAvailable = await this.IsUserAvailableAsync(userData);
                if(isUserAvailable)
                {
                    var isPasswordMatch = _flightManagementContext.Users.AnyAsync(u => u.Login == user.Login && u.Password == user.Password).Result;
                    return isPasswordMatch ? "User Login successful" : $"{user.Login} password did not match";
                }

                return $"No user {user.Login} found";
            }

            return "User data is empty";
        }

        public async Task<string> AddAirlineFlightDetailsAsync(AirlineFlightInputParam airlineFlightInputParam)
        {
            var response = "No Data found";
            if (airlineFlightInputParam?.Airline != null)
            {
                var airline = new Airline
                {
                    AirlineName = airlineFlightInputParam.Airline.AirlineName,
                    IsBlock = false,
                    Logo = airlineFlightInputParam.Airline.Logo
                };

                _flightManagementContext.Airlines.Add(airline);
               await _flightManagementContext.SaveChangesAsync();


                if (airline.AirlineId > 0 && airlineFlightInputParam?.Flight != null)
                {
                    var flight = new Flight
                    {
                        AirlineId = airline.AirlineId,
                        StartTime = airlineFlightInputParam.Flight.StartTime,
                        EndTime = airlineFlightInputParam.Flight.EndTime,
                        FlightNumber = airlineFlightInputParam.Flight.FlightNumber,
                        FromPlace = airlineFlightInputParam.Flight.FromPlace,
                        ToPlace = airlineFlightInputParam.Flight.ToPlace,
                        ScheduledDays = airlineFlightInputParam.Flight.ScheduledDays 
                    };

                    _flightManagementContext.Flights.Add(flight);
                    await _flightManagementContext.SaveChangesAsync();

                    response = "Airline & Flight Data saved successfully !";
                }
                else
                {
                    response = "No flight data found !";
                }                
            }

            return response;
        }

        public async Task<string> AddAirlineInventoryDetailsAsync(InventoryModel inventory)
        {
            var response = "No invetory data found.";
            if (inventory != null)
            {
                var flightId = _flightManagementContext.Flights?.FirstOrDefault(x => x.FlightNumber == inventory.FlightNumber)?.FlightId;

                if (flightId == null || flightId == 0)
                {
                    response = $"Plese add Airline/Flight details for {inventory.FlightNumber}";
                }
                else
                {
                    var isInvetoryAvailable = _flightManagementContext.Inventories
                                                        .AnyAsync(inventory => inventory.FlightId == flightId).Result;
                    var inventoryDetails = new Inventory()
                    {
                        StartTime = inventory.StartTime,
                        EndTime = inventory.EndTime,
                        FromPlace = inventory.FromPlace,
                        ToPlace = inventory.ToPlace,
                        Meal = inventory.Meal,
                        NumberOfRows = inventory.NumberOfRows,
                        ScheduledDays = inventory.ScheduledDays,
                        TotalBusinessClassSeats = inventory.TotalBusinessClassSeats,
                        TotalNonBusinessClassSeats = inventory.TotalNonBusinessClassSeats,
                        TotalTicketCost = inventory.TotalTicketCost,
                        FlightId = flightId ?? 0
                    };

                    if (isInvetoryAvailable)
                    {
                        _flightManagementContext.Inventories.Update(inventoryDetails);
                        await _flightManagementContext.SaveChangesAsync();

                        response = "Invetory records updated successfully";
                    }
                    else
                    {
                        await _flightManagementContext.Inventories.AddAsync(inventoryDetails);
                        await _flightManagementContext.SaveChangesAsync();

                        response = "Invetory records insreted successfully";
                    }                    
                }
            }
            
            return response;
        }

        public async Task<string> BookTicketAsync(int flightId, UserBookingDataModel bookingData)
        {
            var userPassengerList = new List<UserPassengerDatum>();
            string response = string.Empty;
            if (flightId > 0 && bookingData != null && !string.IsNullOrWhiteSpace(bookingData.UserEmail) && !string.IsNullOrWhiteSpace(bookingData.UserName))
            {
                var userbookingData = new UserBookingDatum
                {
                    IsCanceled = false,
                    Meal = bookingData.Meal,
                    MobileNumber = null,
                    PnrNumber = bookingData.UserName + flightId,
                    SeatNumbers = bookingData?.SeatNumbers,
                    TotalBookedSeats = bookingData?.TotalBookedSeats ?? 0,
                    UserEmail = bookingData?.UserEmail ?? string.Empty,
                    UserName = bookingData?.UserName ?? string.Empty                   
                };

                _flightManagementContext.UserBookingData.Add(userbookingData);
                await _flightManagementContext.SaveChangesAsync();

                if (bookingData?.UserPassengerData?.Count > 0)
                {
                    foreach (var passengerData in bookingData.UserPassengerData)
                    {
                        var passenger = new UserPassengerDatum
                        {
                            Age = passengerData.Age,
                            Gender = passengerData.Gender,
                            PassengerName = passengerData.PassengerName,
                            UserBookingDataId = userbookingData.Id
                        };

                        userPassengerList.Add(passenger);
                    }
                }

                _flightManagementContext.UserPassengerData.AddRange(userPassengerList);

                var flightUserMapping = new FlightUserMapping
                {
                    UserId = userbookingData.Id,
                    FlightId = flightId
                };
                _flightManagementContext.FlightUserMappings.Add(flightUserMapping);


                await _flightManagementContext.SaveChangesAsync();                

                return "Records Added successfully!";
            }

            return await Task.FromResult("No data found.");
        }

        public async Task<string> CancelBookedTicketAsync(string pnr)
        {
            var userBookingDetails = await _flightManagementContext.UserBookingData
                                        .FirstOrDefaultAsync(data => data.PnrNumber == pnr && !data.IsCanceled);
            if (userBookingDetails != null)
            {
                userBookingDetails.IsCanceled = true;
                await _flightManagementContext.SaveChangesAsync();

                return "Ticket cancelled scuccessfully !";
            }

            return "Ticket is already cancelled, no changes required !";
        }

        public async Task<UserBookingDataModel> GetBookedTicketsDetailsAsync(string pnr)
        {
            UserBookingDataModel userBookingData = new UserBookingDataModel();

            var result = await _flightManagementContext.UserBookingData.Include(x => x.UserPassengerData)
                                .FirstOrDefaultAsync(x => x.PnrNumber == pnr);

            if (result != null)
            {
                userBookingData.PnrNumber = result.PnrNumber;
                userBookingData.SeatNumbers = result.SeatNumbers;
                userBookingData.UserEmail = result.UserEmail;
                userBookingData.UserName = result.UserName;
                userBookingData.Meal = result.Meal;
                userBookingData.TotalBookedSeats = result.TotalBookedSeats;
                userBookingData.UserPassengerData = result.UserPassengerData.Select(x => new UserPassengerDataModel
                {
                    Age = x.Age,
                    Gender = x.Gender,
                    PassengerName = x.PassengerName
                }).ToList();
            }

            var data = _flightManagementContext.FlightUserMappings.Include(x => x.Flight).ThenInclude(y => y.Airline).ToList();

            return userBookingData;
        }

        public async Task<IList<UserBookingDataModel>> GetBookedTicketsHistoryAsync(string emailId)
        {
            IList<UserBookingDataModel> userBookingDataList = new List<UserBookingDataModel>();

            var result = await _flightManagementContext.UserBookingData.Include(data => data.UserPassengerData)
                                .Where(x => x.UserEmail == emailId).ToListAsync();

            if (result?.Count > 0)
            {
                userBookingDataList = result.Select(bookingData => new UserBookingDataModel
                {
                    UserEmail = bookingData.UserEmail,
                    Meal = bookingData.Meal,
                    PnrNumber = bookingData.PnrNumber,
                    SeatNumbers = bookingData.SeatNumbers,
                    TotalBookedSeats = bookingData.TotalBookedSeats,
                    UserName = bookingData.UserName,
                    UserPassengerData = bookingData.UserPassengerData.Select(x => new UserPassengerDataModel
                    {
                        PassengerName = x.PassengerName,
                        Age = x.Age,
                        Gender = x.Gender
                    }).ToList(),
                }).ToList();
            }

            return await Task.FromResult(userBookingDataList);
        }

        public async Task<IList<FlightModel>> SearchFlightDataAsync(SearchInputParam searchInputParam)
        {
            var flightList = new List<FlightModel>();
            var result = await _flightManagementContext.Flights.ToListAsync();

            if(!string.IsNullOrWhiteSpace(searchInputParam?.FromPlace))
            {
                result = result.Where(x => x.FromPlace == searchInputParam.FromPlace).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchInputParam?.ToPlace))
            {
                result = result.Where(x => x.ToPlace == searchInputParam.ToPlace).ToList();
            }
            if (searchInputParam?.StartTime != null)
            {
                result = result.Where(x => x.StartTime == searchInputParam.StartTime).ToList();
            }
            if (searchInputParam?.EndTime != null)
            {
                result = result.Where(x => x.EndTime == searchInputParam.EndTime).ToList();
            }

            if(result.Count > 0)
            {
                //flightList = result.Select(fli => new FlightModel
                //{
                //    StartTime = fli.StartTime,
                //    EndTime = fli.EndTime,
                //    FlightNumber = fli.FlightNumber,
                //    FromPlace = fli.FromPlace,
                //    ToPlace = fli.ToPlace,
                //    ScheduledDays = fli.ScheduledDays,
                //}).ToList();
                flightList = result.Select(fli => _mapper.Map<FlightModel>(fli)).ToList();
            }            

            return flightList;
        }

        private async Task<bool> IsUserAvailableAsync(User user)
        {
            return  await _flightManagementContext.Users.AnyAsync(u => u.Login == user.Login);
        }
    }
}
