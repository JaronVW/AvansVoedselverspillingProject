using Core.Domain;
using Core.DomainServices;
using Domain;
using Microsoft.EntityFrameworkCore;
using VoedselVerspillingWebApp.Models;

namespace Infrastructure;

public class MealBoxUpdateMethodsRepository : IMealBoxUpdateMethods
{
    private readonly ApplicationDBContext _context;

    public MealBoxUpdateMethodsRepository(
        ApplicationDBContext context)
    {
        _context = context;
    }

    public MealBoxViewModel updateMealBoxGet(int id)
    {
        var m = _context.MealBoxes
            .Include(m => m.Student)
            .Include(m => m.Products)
            .First(b => b.Id == id);

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
        foreach (var p in _context.Products)
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
        var mealBox = new MealBox()
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

        if (mealBoxVm.SelectedProducts != null)
        {
            foreach (var sp in mealBoxVm.SelectedProducts)
            {
                mealBox.Products.Add(_context.Products.Find(sp));
            }
            mealBox.EighteenPlus = mealBox.Products.Any(m => m.ContainsAlcohol);
        }
        
        _context.Remove(_context.MealBoxes.Find(mealBoxVm.Id));
        _context.MealBoxes.Update(mealBox);
        _context.SaveChanges();
        return true;
    }
    
    public MealBoxViewModel formCreateViewModel()
    {
        var vm = new MealBoxViewModel
        {
            PickupDateTime = DateTime.Now,
            ExpireTime = DateTime.Now.AddHours(2).AddDays(1),
            ProductCheckBoxes = new List<CheckBoxItem>()
        };
        foreach (var p in _context.Products)
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