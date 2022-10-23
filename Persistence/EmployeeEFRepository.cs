using System.Globalization;
using Core.DomainServices;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class EmployeeEFRepository : IEmployeeRepository
{
    private readonly ApplicationDBContext _context;

    public EmployeeEFRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public IEnumerable<Employee> GetEmployees()
    {
        return _context.Employees.ToList();
    }

    public Employee GetEmployeeById(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void DeleteEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Employee GetEmployeeByEmail(string email)
    {
        return _context.Employees.Include(e=> e.Canteen).First(e => e.Email == email);
    }
}