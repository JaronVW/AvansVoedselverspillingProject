using Core.DomainServices;
using Domain;

namespace Infrastructure;

public class EmployeeEFRepository : IEmployeeRepository
{
    private ApplicationDBContext _context;

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
        return _context.Employees.First(e => e.Email == email);
    }
}