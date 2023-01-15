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
            .Include(m => m.Products).Where(m => m.StudentId == null)
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


    public void UpdateMealBox(MealBox mealBox, List<Product> products)
    {
        if (mealBox.StudentId != null)
        {
            throw new InvalidFormdataException("Deze maaltijd is al gereserveerd");
        }

        if (mealBox.WarmMeals && _context.Canteens.Find(mealBox.CanteenId)?.WarmMealsprovided != true)
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


        mealBox.Products?.Clear();
        var mealBoxToUpdate = _context.MealBoxes.Include(box => box.Products).First(box => box.Id == mealBox.Id);

        if (products != null)
        {
            if (mealBoxToUpdate.Products != null)
            {
                mealBoxToUpdate.Products.ToList().AddRange(products.Where(x => products.All(y => y.Id != x.Id)));
                foreach (var product in mealBoxToUpdate.Products)
                {
                    if (!products.Contains(product)) mealBoxToUpdate.Products.Remove(product);
                }

                foreach (var product in mealBoxToUpdate.Products)
                {
                    mealBox.EighteenPlus = product.ContainsAlcohol;
                }
            }
        }

        mealBoxToUpdate.MealBoxName = mealBox.MealBoxName;
        mealBoxToUpdate.City = mealBox.City;
        mealBoxToUpdate.Price = mealBox.Price;
        mealBoxToUpdate.PickupDateTime = mealBox.PickupDateTime;
        mealBoxToUpdate.ExpireTime = mealBox.ExpireTime;
        mealBoxToUpdate.WarmMeals = mealBox.WarmMeals;
        mealBoxToUpdate.EighteenPlus = mealBox.EighteenPlus;
        mealBoxToUpdate.CanteenId = mealBox.CanteenId;
        mealBoxToUpdate.StudentId = mealBox.StudentId;

        _context.MealBoxes.Update(mealBoxToUpdate);
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

    public IEnumerable<MealBox> GetMealBoxesOwnCanteen(int canteenId)
    {
        return _context.MealBoxes.Include(m => m.Products).Where(m => m.CanteenId == canteenId)
            .ToList();
    }

    public IEnumerable<MealBox> GetMealBoxesOtherCanteens(int canteenId)
    {
        return _context.MealBoxes.Include(m => m.Products).Where(m => m.CanteenId != canteenId)
            .ToList();
    }
}