using Core.Domain;
using Core.Domain.Exceptions;
using Core.DomainServices;
using Infrastructure.ContextClasses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class MealBoxEFRepository : IMealBoxRepository
{
    private readonly ApplicationDBContext _context;


    public MealBoxEFRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public IEnumerable<MealBox> GetMealBoxesNonReserved()
    {
        return _context.MealBoxes
            .OrderBy(box => box.PickupDateTime)
            .Include(m => m.Products).Where(m => m.StudentId == null)
            .Include(m => m.Student)
            .ToList();
    }

    public IEnumerable<MealBox> GetMealBoxes()
    {
        return _context.MealBoxes
            .OrderBy(box => box.PickupDateTime)
            .Include(m => m.Products)
            .Include(m => m.Student)
            .ToList();
    }

    public IEnumerable<MealBox> GetMealBoxesReserved(int studentId)
    {
        return _context.MealBoxes.Include(m => m.Products).Where(m => m.Student != null && m.StudentId == studentId);
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

    public MealBox GetMealBoxByIdDetached(int id)
    {
        return _context.MealBoxes.AsNoTracking()
            .Include(m => m.Student)
            .Include(m => m.Products)
            .First(b => b.Id == id);
    }

    public MealBox AddMealBox(MealBox mealBox, List<Product> products)
    {
        if (mealBox.WarmMeals && _context.Canteens.Find(mealBox.CanteenId).WarmMealsprovided != true)
        {
            throw new InvalidFormdataException("Warme maaltijden zijn niet beschikbaar in deze kantine");
        }

        if (mealBox.PickupDateTime > DateTime.Now.AddDays(2).AddTicks(-1))
        {
            throw new InvalidFormdataException("De ophaal datum moet binnen nu en twee dagen liggen");
        }

        if (mealBox.PickupDateTime > mealBox.ExpireTime)
        {
            throw new InvalidFormdataException("De ophaal datum moet voor de verloopdatum liggen");
        }

        if (products != null)
        {
            mealBox.Products = products;
            foreach (var mealBoxProduct in mealBox.Products)
            {
                if (mealBoxProduct.ContainsAlcohol) mealBox.EighteenPlus = true;
            }
        }

        _context.MealBoxes.Add(mealBox);
        _context.SaveChanges();
        return mealBox;
    }

    public MealBox AddMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Add(mealBox);
        _context.SaveChanges();
        return mealBox;
    }

    public void UpdateMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Update(mealBox);
        _context.SaveChanges();
    }


    public bool DeleteMealBox(MealBox mealBox)
    {
        try
        {
            _context.MealBoxes.Remove(mealBox);
            _context.SaveChanges();
            return true;
        }
        catch (SqlException e)
        {
            return false;
        }
    }

    public void DeleteMealBoxProducts(MealBox mealBox)
    {
        _context.MealBoxes.Include(mealBox => mealBox.Products).First(mealBox1 => mealBox1.Id == mealBox.Id).Products
            .Clear();
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }


    public MealBox? GetReservedMealBoxToday(int studentId, DateTime date)
    {
        return _context.MealBoxes
            .FirstOrDefault(box => box.StudentId == studentId && box.PickupDateTime.Date == date.Date);
    }

   

   
}