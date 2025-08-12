using DapperDemoData.Data;
using DapperDemoData.Models;

namespace DapperDemoAPI.Endpoints
{
    public static class PersonEndpoints
    {
        public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
        {
            var personGroup = app.MapGroup("persons");
            personGroup.MapGet("/", async (IDataAccess dataAccess) =>
            {
                var query = @"SELECT * FROM Person";
                var persons = await dataAccess.GetData<Person, object>(query, new { });
                return Results.Ok(persons);
            });
            personGroup.MapPost("/", async (IDataAccess dataAccess, Person person) =>
            {
                var query = @"INSERT INTO Person (Name, Email) VALUES (@Name, @Email)";
                await dataAccess.SaveData(query, person);
                return Results.Created($"/api/persons/{person.Id}", person);
            });
        }
    }
}
