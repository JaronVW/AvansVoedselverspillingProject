using Domain;

namespace Core.DomainServices;

public interface IStudentRepository
{
    IEnumerable<Student> GetStudents();

    Student GetStudentById();

}