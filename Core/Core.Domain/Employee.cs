namespace Domain;

public class Employee 
{
    public int Id { get; set; }
    public string FirstName { get; set; }= null!;
    public string LastName { get; set; }= null!;
    public int EmployeeNumber { get; set; }
    public Canteen WorkPlace { get; set; }= null!;
}