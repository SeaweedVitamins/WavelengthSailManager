using System;
namespace WavelengthSailManager.Models
{
    public class RaceManagementModel
    {
        public int Race_Number { get; set; }
        public DateTime DateTime { get; set; }
        public string Series_Name { get; set; }
        public string Category_Name { get; set; }
        public string Special_Event { get; set; }
    }
}
