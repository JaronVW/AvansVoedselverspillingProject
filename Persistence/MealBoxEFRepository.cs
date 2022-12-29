using Core.DomainServices;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VoedselVerspillingWebApp.Models;

namespace Infrastructure;

public class MealBoxEFRepository : IMealBoxRepository
{
    private readonly ApplicationDBContext _context;


    public MealBoxEFRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public IEnumerable<MealBox> GetMealBoxes()
    {
        return _context.MealBoxes
            .OrderBy(box => box.PickupDateTime)
            .Include(m => m.Products)
            .ToList();
    }

    public IEnumerable<MealBox> GetMealBoxesDateAscending()
    {
        return _context.MealBoxes.Include(m => m.Products).ToList();
    }

    public async Task<List<MealBox>> GetMealBoxesAsync()
    {
        return await _context.MealBoxes
            .Include(m => m.Student)
            .Include(m => m.Products)
            .ToListAsync();
    }

    public IEnumerable<MealBox> GetMealBoxesEighteenPlus()
    {
        return _context.MealBoxes.Where(b => b.EighteenPlus).ToList();
    }

    public MealBox GetMealBoxById(int id)
    {
        return _context.MealBoxes
            .Include(m => m.Student)
            .Include(m => m.Products)
            .First(b => b.Id == id);
    }

    public MealBox AddMealBox(MealBoxViewModel mealBoxVm)
    {
        try
        {
            MealBox mealBox = new MealBox()
            {
                MealBoxName = mealBoxVm.MealBoxName,
                City = mealBoxVm.City,
                PickupDateTime = mealBoxVm.PickupDateTime,
                ExpireTime = mealBoxVm.ExpireTime,
                EighteenPlus = mealBoxVm.EighteenPlus,
                Price = mealBoxVm.Price,
                Type = mealBoxVm.Type,
                CanteenId = mealBoxVm.CanteenId,
                Products = new List<Product>(),
                WarmMeals = mealBoxVm.WarmMeals
            };
            
            foreach (var sp in mealBoxVm.selectedProducts)
            {
                var mb = _context.Products.Find(sp);
                mealBox.Products.Add(mb);
                if (mb.ContainsAlcohol)
                {
                    mealBox.EighteenPlus = true;
                }
            }
            _context.MealBoxes.Add(mealBox);
            _context.SaveChanges();
            return mealBox;
        }
        catch
        {
            return null;
        }
    }


    public void UpdateMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Update(mealBox);
        _context.SaveChanges();
    }


    public void DeleteMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Remove(mealBox);
        _context.SaveChanges();
    }

    public void DeleteMealBoxProducts(MealBox mealBox)
    {
        _context.MealBoxes.First(m => m.Id == mealBox.Id).Products = null;
        _context.SaveChanges();
    }

    public void DeleteMealBoxById(int id)
    {
        _context.MealBoxes.Remove(_context.MealBoxes.Find(id));
        _context.SaveChanges();
    }


    public MealBox? GetReservedMealBoxToday(int studentId, DateTime date)
    {
        return _context.MealBoxes
            .FirstOrDefault(box => box.StudentId == studentId && box.PickupDateTime.Date == date.Date);
    }

    public void ReserveMealBox(int mealBoxId, int studentId)
    {
        var recordToUpdate = _context.MealBoxes.FirstOrDefault(m => m.Id == mealBoxId);
        recordToUpdate.Student = _context.Students.FirstOrDefault(m => m.Id == studentId);
        _context.MealBoxes.Update(recordToUpdate);
        _context.SaveChanges();
    }
}