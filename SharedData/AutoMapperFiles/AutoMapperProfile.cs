using AutoMapper;
using FlightBookingRepository.DataModels;
using SharedData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.AutoMapperFiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<AirlineModel, Airline>();
            CreateMap<Flight, FlightModel>();
            CreateMap<FlightModel, Flight>();
        }
    }
}
