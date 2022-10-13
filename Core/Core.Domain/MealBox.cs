namespace Domain
{
    public class MealBox
    {
        public int Id { get; set; }
        
        public string MealBoxName { get; set; }

        public City City { get; set; }
        
        public DateTime PickupDateTime  { get; set; }
        
        public DateTime ExpireTime { get; set; }
        
        public bool EighteenPlus { get; set; }
        
        public decimal Price { get; set; }
        
        public MealType Type { get; set; }

        public Student Student { get; set; }
        
        public int? StudentId { get; set; }
        
        public ICollection<Product> Products { get; set; }= null!;
        
        public Canteen Canteen { get; set; }= null!;
        
        public int CanteenId { get; set; }
        
    }
}