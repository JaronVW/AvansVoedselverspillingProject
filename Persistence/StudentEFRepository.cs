using Core.Domain;
using Core.DomainServices;
using Infrastructure.ContextClasses;

namespace Infrastructure;

public class StudentEFRepository :IStudentRepository
{

    private readonly ApplicationDBContext _context;
    
    public StudentEFRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Student> GetStudents()
    {
        return _context.Students.ToList();
    }
    
    public Student GetStudentById(int id)
    {
         return _context.Students.First(s => s.Id == id);
    }
    
    public Student GetStudentByEmail(string email)
    {
        return  _context.Students.FirstOrDefault(s => s.email == email);
    }

    public void UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void DeleteStudent(Student student)
    {
        _context.Students.Remove(student);
        _context.SaveChanges();
    }

    public void AddStudent(Student student)
    {
        _context.Add(student);
        _context.SaveChanges();
    }
}