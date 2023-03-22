using System;
using System.Collections.Generic;

namespace SharedData.Models;

public partial class UserBookingDataModel
{
    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    //public int? MobileNumber { get; set; }

    public int TotalBookedSeats { get; set; }

    public string? SeatNumbers { get; set; }

    public string? Meal { get; set; }

    //public bool IsCanceled { get; set; }

    public string? PnrNumber { get; set; }

    public virtual IList<UserPassengerDataModel> UserPassengerData { get; set; } = new List<UserPassengerDataModel>();
}
