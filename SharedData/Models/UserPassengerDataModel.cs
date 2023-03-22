using System;
using System.Collections.Generic;

namespace SharedData.Models;

public partial class UserPassengerDataModel
{
    public string PassengerName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }   
}
