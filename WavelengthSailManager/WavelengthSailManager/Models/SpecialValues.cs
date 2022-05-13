using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class SpecialValues
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
