﻿using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class Configuration
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
