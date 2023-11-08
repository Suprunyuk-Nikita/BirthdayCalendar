using SQLite;

namespace BirthdayCalendar.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
    }
}
