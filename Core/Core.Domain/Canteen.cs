namespace Domain
{
    public class Canteen
    {
        public int Id { get; set; }
        
        public City City { get; set; }
        
        public string Address { get; set; }
        
        public string PostalCode { get; set; }
        
        public bool WarmMealsprovided { get; set; }
    }
}