using Core.DomainServices;
using Domain;
using VoedselVerspillingWebApp.Models;

namespace Infrastructure;

public class MealBoxupdateMethods : IMealBoxUpdateMethods
{
    private readonly IMealBoxRepository _mealBoxRepository;

    private readonly IProductRepository _productRepository;

    public MealBoxupdateMethods(IMealBoxRepository mealBoxRepository,  IProductRepository productRepository)
    {
        _mealBoxRepository = mealBoxRepository;

        _productRepository = productRepository;
    }

    public MealBoxViewModel updateMealBoxGet(int id)
    {
        var m = _mealBoxRepository.GetMealBoxById(id);

        var vm = new MealBoxViewModel
        {
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

    public bool updateMealBoxPost(MealBoxViewModel mealBoxVm)
    {
        if (mealBoxVm.StudentId != null) return false;
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
            Products = new List<Product>()
        };

        foreach (var sp in mealBoxVm.selectedProducts)
        {
            mealBox.Products.Add(_productRepository.GetProductById(sp));
        }


        _mealBoxRepository.DeleteMealBox(_mealBoxRepository.GetMealBoxById(mealBoxVm.Id));
        mealBox.EighteenPlus = mealBox.Products.Any(m => m.ContainsAlcohol);
        _mealBoxRepository.UpdateMealBox(mealBox);
        return true;
    }
}