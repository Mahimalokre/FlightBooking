using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.Models;

public class SearchInputParam
{
    public string FromPlace { get; set; } = null!;

    public string ToPlace { get; set; } = null!;

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool IsOneWayFlight { get; set; }

    public bool IsRoundWayFlight { get; set; }
}

public class AirlineFlightInputParam
{
    public AirlineModel Airline {get; set;}
    public FlightModel Flight { get; set; }
}