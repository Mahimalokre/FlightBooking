using System;
using System.Collections.Generic;

namespace SharedData.Models;

public partial class AirlineModel
{
    public string AirlineName { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public bool IsBlock { get; set; }
}