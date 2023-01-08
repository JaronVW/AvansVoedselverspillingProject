using Core.Domain;
using Core.Domain.Exceptions;
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
        if (mealBoxVm.WarmMeals && _context.Canteens.Find(mealBoxVm.CanteenId).WarmMealsprovided != true)
        {
            throw new InvalidFormdataException("Warme maaltijden zijn niet beschikbaar in deze kantine");
        }

        if (mealBoxVm.PickupDateTime > DateTime.Now.AddDays(2).AddTicks(-1))
        {
            throw new InvalidFormdataException("De ophaal datum moet binnen nu en twee dagen liggen");
        }

        if (mealBoxVm.PickupDateTime > mealBoxVm.ExpireTime)
        {
            throw new InvalidFormdataException("De ophaal datum moet voor de verloopdatum liggen");
        }

        var mealBox = new MealBox()
        {
            MealBoxName = mealBoxVm.MealBoxName,
            City = mealBoxVm.City,
            PickupDateTime = mealBoxVm.PickupDateTime,
            ExpireTime = mealBoxVm.ExpireTime,
            EighteenPlus = false,
            Price = mealBoxVm.Price,
            Type = mealBoxVm.Type,
            CanteenId = mealBoxVm.CanteenId,
            Products = new List<Product>(),
            WarmMeals = mealBoxVm.WarmMeals
        };

        if (mealBoxVm.SelectedProducts != null)
        {
            foreach (var mb in mealBoxVm.SelectedProducts.Select(sp => _context.Products.Find(sp)))
            {
                if (mb == null) continue;
                mealBox.Products.Add(mb);
                if (mb.ContainsAlcohol)
                {
                    mealBox.EighteenPlus = true;
                }
            }
        }

        _context.MealBoxes.Add(mealBox);
        _context.SaveChanges();
        return mealBox;
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
        var m = _context.MealBoxes.Find(mealBoxId);
        var s = _context.Students.Find(studentId);

        if (s.BirthDate.Date > m.PickupDateTime.AddYears(-18) && m.EighteenPlus)
        {
            throw new InvalidReservationException("U moet achttien zijn om deze maaltijdbox te reserveren");
        }

        if (GetReservedMealBoxToday(studentId, m.PickupDateTime) != null)
        {
            throw new InvalidReservationException("U heeft al een maaltijdbox voor deze dag gereserveerd");
        }

        m.StudentId = s.Id;
        _context.MealBoxes.Update(m);
        _context.SaveChanges();
    }

    public bool ReserveMealBoxCancel(int mealBoxId)
    {
        try
        {
            var m = _context.MealBoxes.Find(mealBoxId);
            m.StudentId = null;
            _context.Update(m);
            _context.SaveChanges();
            return true;
        }
        catch 
        {
            return false;
        }
     
    }
}