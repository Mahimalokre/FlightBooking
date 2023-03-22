using System;
using System.Collections.Generic;

namespace SharedData.Models;

public partial class UserModel
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
