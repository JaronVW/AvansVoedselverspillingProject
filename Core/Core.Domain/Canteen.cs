namespace Domain
{
    public class Canteen
    {
        public int Id { get; set; }
        
        public City City { get; set; }
        
        public string Address { get; set; }= null!;
        
        public string PostalCode { get; set; }= null!;
        
        public bool? WarmMealsprovided { get; set; }
    }
}