using System;
using System.Collections.Generic;

namespace FlightBookingRepository.DataModels;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<FlightUserMapping> FlightUserMappings { get; } = new List<FlightUserMapping>();
}
