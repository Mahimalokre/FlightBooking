using System;
using System.Collections.Generic;

namespace SharedData.Models;

public partial class InventoryModel
{    
    public string FlightNumber { get; set; } = null!;

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
}
