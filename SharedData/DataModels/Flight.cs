using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public string FromPlace { get; set; } = null!;

    public string ToPlace { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ScheduledDays { get; set; } = null!;

    public int AirlineId { get; set; }

    public virtual Airline Airline { get; set; } = null!;

    public virtual ICollection<FlightUserMapping> FlightUserMappings { get; } = new List<FlightUserMapping>();

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();
}
