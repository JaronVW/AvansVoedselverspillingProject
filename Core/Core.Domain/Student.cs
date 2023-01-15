using Core.Domain.Enums;

namespace Core.Domain
{
    public class Student 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }= null!;
        public string LastName { get; set; }= null!;
        public DateTime BirthDate { get; set; }
        
        public int StudentNumber { get; set; }

        public string email { get; set; }= null!;

        public City StudyCity { get; set; }

        public string PhoneNumber { get; set; }= null!;
    }
}