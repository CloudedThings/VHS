﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VHS.Entity
{
    public class Vehicle
        {
            public string Vin { get; set; }
            public string RegNo { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }
            public string Color { get; set; }
            public Owner Owner { get; set; }
        }
    }
