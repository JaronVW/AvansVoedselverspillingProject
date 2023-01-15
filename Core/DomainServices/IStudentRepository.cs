using Core.Domain;

namespace Core.DomainServices;

public interface IStudentRepository
{
    IEnumerable<Student> GetStudents();

    Student GetStudentById(int id);

    void UpdateStudent(Student student);

    void DeleteStudent(Student student);

    void AddStudent(Student student);

    Student GetStudentByEmail(string email);

}