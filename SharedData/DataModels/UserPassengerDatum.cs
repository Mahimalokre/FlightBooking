using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class UserPassengerDatum
{
    public int Id { get; set; }

    public string PassengerName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public int UserBookingDataId { get; set; }

    public virtual UserBookingDatum UserBookingData { get; set; } = null!;
}
