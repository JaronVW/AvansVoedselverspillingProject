using System;
using System.Collections.Generic;

namespace Domain
{
    public class MealBox
    {
        public int Id { get; set; }
        
        public City City { get; set; }
        
        public DateTime PickupDateTime  { get; set; }
        
        public DateTime ExpireTime { get; set; }
        
        public bool EightteenPlus { get; set; }
        
        public decimal Price { get; set; }
        
        public MealType Type { get; set; }
        
        public Student Student { get; set; }
        
        public List<Product> Products { get; set; }
        
        public Cantine Cantine { get; set; }
        
    }
}