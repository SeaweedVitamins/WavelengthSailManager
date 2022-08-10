using SQLite;

namespace WavelengthSailManager.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Category_Name { get; set; }
    }
}