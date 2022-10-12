using Core.DomainServices;
using Domain;

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
        return _Context.Canteens.First(c => c.Id == id );
    }

    public void UpdateCanteen(Canteen canteen)
    {
        _Context.Canteens.Update(canteen);
    }

    public void DeleteCanteen(Canteen canteen)
    {
        _Context.Canteens.Remove(canteen);
    }

    public void AddCanteen(Canteen canteen)
    {
        _Context.Canteens.Add(canteen);
    }

   
}