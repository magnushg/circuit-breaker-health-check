﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public enum HealthState
    {
        OK,
        TryingToRestablish,
        Critical
    }
}