using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public string FromPlace { get; set; } = null!;

    public string ToPlace { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ScheduledDays { get; set; } = null!;

    public int? TotalBusinessClassSeats { get; set; }

    public int? TotalNonBusinessClassSeats { get; set; }

    public decimal TotalTicketCost { get; set; }

    public int NumberOfRows { get; set; }

    public string Meal { get; set; } = null!;

    public int FlightId { get; set; }

    public virtual Flight Flight { get; set; } = null!;
}
