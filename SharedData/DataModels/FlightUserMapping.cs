using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class FlightUserMapping
{
    public int Id { get; set; }

    public int FlightId { get; set; }

    public int UserId { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
