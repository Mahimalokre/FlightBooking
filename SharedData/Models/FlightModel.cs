using System;
using System.Collections.Generic;

namespace SharedData.Models;

public partial class FlightModel
{    
    public string FlightNumber { get; set; } = null!;

    public string FromPlace { get; set; } = null!;

    public string ToPlace { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ScheduledDays { get; set; } = null!;
}
