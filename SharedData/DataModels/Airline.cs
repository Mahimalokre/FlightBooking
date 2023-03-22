using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class Airline
{
    public int AirlineId { get; set; }

    public string AirlineName { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public bool IsBlock { get; set; }

    public virtual ICollection<Flight> Flights { get; } = new List<Flight>();
}
