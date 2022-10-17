using Core.DomainServices;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<MealBox>> GetMealBoxesAsync()
    {
        return await _context.MealBoxes.ToListAsync();
    }

    public IEnumerable<MealBox> GetMealBoxesEighteenPlus()
    {
        return _context.MealBoxes.Where(b => b.EighteenPlus).ToList();
    }

    public MealBox GetMealBoxById(int id)
    {
        return _context.MealBoxes.First(b => b.Id == id);
    }


    public async Task AddMealBox(MealBox mealBox)
    {
        try
        {
            _context.MealBoxes.Add(mealBox);
            await _context.SaveChangesAsync();
        }
        catch (SqlException ex)
        {
    
            foreach (SqlError error in ex.Errors)
            {
               Console.WriteLine(error);
            }
        }
    }


    public async void UpdateMealBox(MealBox mealBox)
    {
        var recordToUpdate = _context.MealBoxes.FirstOrDefault(m => m.Id == mealBox.Id);
        recordToUpdate = mealBox;
        _context.MealBoxes.Update(recordToUpdate);
        await _context.SaveChangesAsync();
    }


    public async void DeleteMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Remove(mealBox);
        await _context.SaveChangesAsync();
    }

    public ICollection<Product> GetMealBoxProducts(int id)
    {
        return _context.MealBoxes.First(m => m.Id == id).Products;
    }

    public async void ReserveMealBox(int mealBoxId, int studentId)
    {
        var recordToUpdate = _context.MealBoxes.FirstOrDefault(m => m.Id == mealBoxId);
        recordToUpdate.Student = _context.Students.FirstOrDefault(m => m.Id == studentId);
        _context.MealBoxes.Update(recordToUpdate);
        await _context.SaveChangesAsync();
    }
}