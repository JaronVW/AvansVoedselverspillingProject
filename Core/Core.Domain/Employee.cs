﻿namespace Core.Domain;

public class Employee 
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }= null!;
    public string LastName { get; set; }= null!;
    public int EmployeeNumber { get; set; }
    public Canteen Canteen { get; set; }= null!;
    public int CanteenId { get; set; }
    
}