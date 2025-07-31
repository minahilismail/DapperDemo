using DapperDemoData.Data;
using DapperDemoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemoData.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDataAccess _db;
        public PersonRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                string query = @"INSERT INTO Person (Name, Email) 
                            VALUES (@Name, @Email)";
                await _db.SaveData(query, new { person.Name, person.Email });
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                string query = @"DELETE FROM Person WHERE Id = @Id";
                await _db.SaveData(query, new { Id = id });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            try
            {
                string query = @"SELECT * FROM Person";
                var people = await _db.GetData<Person,dynamic>(query, new { });
                return people;
            }
            catch (Exception)
            {

                return Enumerable.Empty<Person>();
            }
        }

        public async Task<Person> GetPersonById(int id)
        {
            try
            {
                string query = @"SELECT * FROM Person WHERE Id = @Id";
                var person = await _db.GetData<Person, dynamic>(query, new { Id = id });
                return person.FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                string query = @"UPDATE Person
                            SET Name = @Name, Email = @Email 
                            WHERE Id = @Id";
                await _db.SaveData(query, person);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
