using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repository;

public class GroupRepository
{
    private readonly IConfiguration _configuration;

    public GroupRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> doesGroupExist(int id)
    {
        var query = "Select 1 from Groups where id = @id";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }

    
    public async Task<GroupDTO> getGroup(int id)
    {
        var query = "select G.ID as ID, G.Name as Name, S.ID as Students" +
                    " from Groups G join GroupAssignments GA on G.ID = GA.Group_ID join Students S on GA.Student_ID = S.ID" +
                    " where g.id = @id;";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);

        connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        var groupIdOrdinal = reader.GetOrdinal("ID");
        var gropNameOrdinal = reader.GetOrdinal("Name");
        var studentsOrdinal = reader.GetOrdinal("Students");

        GroupDTO groupDto = null;

        while (await reader.ReadAsync())
        {
            if (groupDto is not null)
            {
                groupDto.Students.Add(new Student()
                    {id = reader.GetInt32(studentsOrdinal)}
                );
            }
            else
            {
                groupDto = new GroupDTO()
                {
                    id = reader.GetInt32(groupIdOrdinal),
                    name = reader.GetString(gropNameOrdinal),
                    Students = new List<Student>() {
                        new Student() { id = reader.GetInt32(studentsOrdinal) }
                    }
                    
                };
            }
        }

        if (groupDto is null) throw new Exception();

        return groupDto;

    }

}