namespace WebApplication2.Models;

public class GroupDTO
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public List<Student> Students { get; set; } = null;
}

public class Student
{
    public int id { get; set; }
}

