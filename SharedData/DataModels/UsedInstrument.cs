using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class UsedInstrument
{
    public int InstrumentId { get; set; }

    public string InstrumentName { get; set; } = null!;

    public bool? IsActive { get; set; }
}
