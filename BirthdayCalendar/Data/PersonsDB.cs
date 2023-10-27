using System.Collections.Generic;
using System.Threading.Tasks;
using BirthdayCalendar.Models;
using SQLite;

namespace BirthdayCalendar.Data
{
    class PersonsDB
    {
        readonly SQLiteAsyncConnection db;

        public PersonsDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> GetPersonsAsync()
        {
            return db.Table<Person>().ToListAsync();
        }

        public Task<Person> GetPersonAsync(int id)
        {
            return db.Table<Person>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }
        
        public Task<int> SavePersonAsync(Person person)
        {
            if(person.ID != 0)
            {
                return db.UpdateAsync(person);
            }
            else
            {
                return db.InsertAsync(person);
            }
        }

        public Task<int> DeletePersonAsync(Person person)
        {
            return db.DeleteAsync(person);
        }
    }
}
