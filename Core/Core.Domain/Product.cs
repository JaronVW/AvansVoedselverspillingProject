namespace Core.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool ContainsAlcohol { get; set; }
        
        public string Photo { get; set; }= null!;
        
        public ICollection<MealBox> MealBoxes { get; set; }= null!;
    }
}