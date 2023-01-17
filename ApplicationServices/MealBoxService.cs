using Core.Domain;
using Core.Domain.Exceptions;
using Core.DomainServices;

namespace ApplicationServices;

public class MealBoxService : IMealBoxService
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;

    public MealBoxService(IMealBoxRepository mealBoxRepository,
        ICanteenRepository canteenRepository, IStudentRepository studentRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
    }

    public IEnumerable<MealBox> GetMealBoxesNonReserved()
        => _mealBoxRepository.GetMealBoxes()
            .OrderBy(box => box.PickupDateTime)
            .Where(m => m.StudentId == null)
            .ToList();


    public IEnumerable<MealBox> GetMealBoxesReserved(int studentId)
        => _mealBoxRepository.GetMealBoxes().Where(m => m.StudentId == studentId);


    public MealBox AddMealBox(MealBox mealBox, List<Product> products, int canteenId)
    {
        var canteen = _canteenRepository.GetCanteenById(canteenId);
        if (mealBox.WarmMeals && canteen.WarmMealsprovided != true)
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


        mealBox.CanteenId = canteen.Id;
        mealBox.Canteen = canteen;
        mealBox.City = canteen.City;


        if (products != null)
        {
            mealBox.Products = products;
            foreach (var mealBoxProduct in mealBox.Products)
            {
                if (mealBoxProduct.ContainsAlcohol) mealBox.EighteenPlus = true;
            }
        }

        _mealBoxRepository.AddMealBox(mealBox);
        return mealBox;
    }

    public MealBox UpdateMealBox(MealBox mealBox, List<Product> products)
    {
        if (mealBox.StudentId != null)
        {
            throw new InvalidFormdataException("Deze maaltijd is al gereserveerd");
        }

        if (mealBox.WarmMeals && _canteenRepository.GetCanteenById(mealBox.CanteenId)?.WarmMealsprovided != true)
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

        _mealBoxRepository.DeleteMealBoxProducts(mealBox);

        if (products != null)
        {
            foreach (var product in products)
            {
                mealBox.Products.Add(product);
            }

            foreach (var mealBoxProduct in mealBox.Products)
            {
                if (mealBoxProduct.ContainsAlcohol) mealBox.EighteenPlus = true;
            }
        }

        _mealBoxRepository.UpdateMealBox(mealBox);
        return mealBox;
    }

    public bool DeleteMealBox(int id)
    {
        var mealBox = _mealBoxRepository.GetMealBoxById(id);
        if (mealBox.StudentId != null) return false;
        _mealBoxRepository.DeleteMealBox(mealBox);
        return true;
    }

    public bool ReserveMealBox(int mealBoxId, int studentId)
    {
        var m = _mealBoxRepository.GetMealBoxById(mealBoxId);
        var s = _studentRepository.GetStudentById(studentId);

        if (m.StudentId != null) return false;

        if (s.BirthDate.Date > m.PickupDateTime.AddYears(-18) && m.EighteenPlus)
        {
            throw new InvalidReservationException("U moet achttien zijn om deze maaltijdbox te reserveren");
        }

        if (_mealBoxRepository.GetReservedMealBoxToday(studentId, m.PickupDateTime) != null)
        {
            throw new InvalidReservationException("U heeft al een maaltijdbox voor deze dag gereserveerd");
        }

        m.StudentId = s.Id;
        _mealBoxRepository.UpdateMealBox(m);
        return true;
    }

    public bool ReserveMealBoxCancel(int mealBoxId, int studentId)
    {
        var m = _mealBoxRepository.GetMealBoxById(mealBoxId);
        if (m.StudentId != studentId) return false;
        try
        {
            m.StudentId = null;
            _mealBoxRepository.UpdateMealBox(m);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<MealBox> GetMealBoxesOwnCanteen(int canteenId)
        => _mealBoxRepository.GetMealBoxes().Where(m => m.CanteenId == canteenId && m.StudentId == null)
            .OrderBy(box => box.PickupDateTime)
            .ToList();


    public IEnumerable<MealBox> GetMealBoxesOtherCanteens(int canteenId)
        => _mealBoxRepository.GetMealBoxes().Where(m => m.CanteenId != canteenId && m.StudentId == null)
            .OrderBy(box => box.PickupDateTime)
            .ToList();
}