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


    public IEnumerable<MealBox> GetMealBoxes()
    {
        return _context.MealBoxes
            .OrderBy(box => box.PickupDateTime)
            .Include(m => m.Products)
            .Include(m => m.Student)
            .ToList();
    }


    public MealBox GetMealBoxById(int id)
    {
        return _context.MealBoxes
            .Include(m => m.Student)
            .Include(m => m.Products)
            .First(b => b.Id == id);
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

    public MealBox ReserveMealBox(int mealBoxId,int studentId )
    {
        var mealBox = _context.MealBoxes.Include(box => box.Products).Include(box => box.Student)
            .FirstOrDefault(box => box.Id == mealBoxId);
        if(mealBox == null) throw new NullReferenceException("MealBox not found");
        if (mealBox.StudentId != null)
        {
            throw new InvalidReservationException("MealBox is already reserved");
        }

        mealBox.StudentId = studentId;
        _context.SaveChanges();
        return mealBox;
    }
}