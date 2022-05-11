﻿using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class SpecialValues
    {
        [PrimaryKey, AutoIncrement]
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
