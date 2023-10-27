using System;
using SQLite;

namespace BirthdayCalendar.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string image { get; set; }
        public DateTime date { get; set; }
    }
}
