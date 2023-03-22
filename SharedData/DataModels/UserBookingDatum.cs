using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class UserBookingDatum
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public int? MobileNumber { get; set; }

    public int TotalBookedSeats { get; set; }

    public string? SeatNumbers { get; set; }

    public string? Meal { get; set; }

    public bool IsCanceled { get; set; }

    public string? PnrNumber { get; set; }

    public virtual ICollection<UserPassengerDatum> UserPassengerData { get; } = new List<UserPassengerDatum>();
}
