﻿using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class PY
    {
        [PrimaryKey, AutoIncrement]
        public string Class_Name { get; set; }
        public string Value { get; set; }
    }
}
