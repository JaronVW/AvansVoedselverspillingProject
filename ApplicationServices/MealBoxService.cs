using Core.Domain;
using Core.Domain.Exceptions;
using Core.DomainServices;
using Domain;
using VoedselVerspillingWebApp.Models;

public class MealBoxService : IMealBoxService
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICanteenRepository _canteenRepository;

    public MealBoxService(IMealBoxRepository mealBoxRepository, IProductRepository productRepository,
        ICanteenRepository canteenRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _productRepository = productRepository;
        _canteenRepository = canteenRepository;
    }


    public MealBox AddMealBox(MealBox mealBox, List<Product> products)
    {
        if (mealBox.WarmMeals && _canteenRepository.GetCanteenById(mealBox.CanteenId).WarmMealsprovided != true)
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

        _mealBoxRepository.AddMealBox(mealBox);
        return mealBox;
    }

    public MealBoxViewModel UpdateMealBoxGet(int id)
    {
        var m = _mealBoxRepository.GetMealBoxById(id);
        if (m.StudentId != null)
        {
            throw new Exception("MealBox is already assigned to a student");
        }

        var vm = new MealBoxViewModel
        {
            Id = m.Id,
            MealBoxName = m.MealBoxName,
            City = m.City,
            PickupDateTime = m.PickupDateTime,
            ExpireTime = m.ExpireTime,
            EighteenPlus = m.EighteenPlus,
            Price = m.Price,
            CanteenId = m.CanteenId,
            StudentId = m.StudentId,
            ProductCheckBoxes = new List<CheckBoxItem>()
        };
        foreach (var p in _productRepository.GetProducts())
        {
            if (m.Products.Contains(p))
            {
                vm.ProductCheckBoxes.Add(new CheckBoxItem()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsChecked = true
                });
            }
            else
            {
                vm.ProductCheckBoxes.Add(new CheckBoxItem()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsChecked = false
                });
            }
        }

        return vm;
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
}