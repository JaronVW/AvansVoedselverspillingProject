using Core.Domain;
using Core.DomainServices;

namespace VoedselVerspillingWebApp.extensions;

public static class MealBoxViewModeleExtension
{
    public static MealBoxViewModel updateMealBoxGet(this IMealBoxRepository mealBoxRepository, int id,
        IEnumerable<Product> products)
    {
        var mealBox = mealBoxRepository.GetMealBoxById(id);

        if (mealBox.StudentId != null)
        {
            throw new Exception("MealBox is already assigned to a student");
        }

        var vm = new MealBoxViewModel
        {
            Id = mealBox.Id,
            MealBoxName = mealBox.MealBoxName,
            City = mealBox.City,
            PickupDateTime = mealBox.PickupDateTime,
            ExpireTime = mealBox.ExpireTime,
            EighteenPlus = mealBox.EighteenPlus,
            Price = mealBox.Price,
            CanteenId = mealBox.CanteenId,
            StudentId = mealBox.StudentId,
            ProductCheckBoxes = new List<CheckBoxItem>(),
            Type = mealBox.Type
        };
        foreach (var p in products)
        {
            if (mealBox.Products.Contains(p))
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
    
    public static MealBoxViewModel formCreateViewModel(IEnumerable<Product> products)
    {
        var vm = new MealBoxViewModel
        {
            PickupDateTime = DateTime.Now,
            ExpireTime = DateTime.Now.AddDays(1),
            ProductCheckBoxes = new List<CheckBoxItem>()
        };
        foreach (var p in products)
        {
            vm.ProductCheckBoxes.Add(new CheckBoxItem()
            {
                Id = p.Id,
                Name = p.Name,
                IsChecked = false
            });
        }

        return vm;
    }
}