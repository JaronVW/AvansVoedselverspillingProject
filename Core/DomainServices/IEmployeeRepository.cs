using Core.Domain;

namespace Core.DomainServices;

public interface IEmployeeRepository
{
    
    IEnumerable<Employee> GetEmployees();

    Employee GetEmployeeById(int id);

    void UpdateEmployee(Employee employee);

    void DeleteEmployee(Employee employee);

    void AddEmployee(Employee employee);

    Employee GetEmployeeByEmail(string email);
}