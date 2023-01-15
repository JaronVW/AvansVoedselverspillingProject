using Core.Domain;
using Core.DomainServices;
using Infrastructure.ContextClasses;

namespace Infrastructure;

public class CanteenEFRepository : ICanteenRepository
{
    private ApplicationDBContext _Context;

    public CanteenEFRepository(ApplicationDBContext context)
    {
        _Context = context;
    }

    public IEnumerable<Canteen> GetCanteens()
    {
        return _Context.Canteens.ToList();
    }


    public Canteen GetCanteenById(int id)
    {
        return _Context.Canteens.First(c => c.Id == id);
    }

    public async void UpdateCanteen(Canteen canteen)
    {
        _Context.Canteens.Update(canteen);
        await _Context.SaveChangesAsync();
    }

    public async  void DeleteCanteen(Canteen canteen)
    {
        _Context.Canteens.Remove(canteen);
        await _Context.SaveChangesAsync();
    }

    public async void AddCanteen(Canteen canteen)
    {
        _Context.Canteens.Add(canteen);
        await _Context.SaveChangesAsync();
    }
}