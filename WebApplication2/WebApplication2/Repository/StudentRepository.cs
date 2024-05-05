using Microsoft.Data.SqlClient;

namespace WebApplication2.Repository;

public class StudentRepository
{
    private readonly IConfiguration _configuration;

    public StudentRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
    public async Task<bool> doesStudentExist(int id)
    {
        var query = "Select 1 from Students where id = @id";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }

    public async Task deleteStudent(int id)
    {
        var query = "delete from GroupAssignments where Student_ID=@id";
        var query1 = "DELETE from Students where id = @id";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        command.CommandText = query1;

        await command.ExecuteNonQueryAsync();

    }
}