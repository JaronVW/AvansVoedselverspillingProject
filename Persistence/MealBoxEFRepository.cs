﻿using Core.DomainServices;
using Domain;

namespace Infrastructure;

public class MealBoxEFRepository : IMealBoxRepository
{

    private ApplicationDBContext _context;
    
    
    public MealBoxEFRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public IEnumerable<MealBox> GetMealBoxes()
    {
        return _context.MealBoxes.ToList();
    }
    
    public IEnumerable<MealBox> GetMealBoxesEighteenPlus()
    {
        return _context.MealBoxes.Where(b => b.EighteenPlus  ).ToList();
    }

    public MealBox GetMealBoxById(int id)
    {
        return _context.MealBoxes.First(b => b.Id == id);
    }

    public async void AddMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Add(mealBox);
        await _context.SaveChangesAsync();
    }
    

    public void UpdateMealBox(MealBox mealBox)
    {
        var recordToUpdate = _context.MealBoxes.FirstOrDefault(m => m.Id == mealBox.Id);
        recordToUpdate = mealBox;
        _context.MealBoxes.Update(recordToUpdate);
        _context.SaveChanges();
    }
    

    public void DeleteMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Remove(mealBox);
        _context.SaveChanges();
    }

    public ICollection<Product> GetMealBoxProducts(int id)
    {
        return _context.MealBoxes.First(m => m.Id == id).Products;
    }

    public void ReserveMealBox(int mealBoxId, int studentId)
    { 
        var recordToUpdate = _context.MealBoxes.FirstOrDefault(m => m.Id == mealBoxId);
        recordToUpdate.Student = _context.Students.FirstOrDefault(m => m.Id == studentId);
        _context.MealBoxes.Update(recordToUpdate);
        _context.SaveChanges();
    }
    
}