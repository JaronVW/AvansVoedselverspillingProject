using Core.Domain;

namespace Core.DomainServices;

public interface ICanteenRepository
{
    IEnumerable<Canteen> GetCanteens();

    Canteen GetCanteenById(int id);

    void UpdateCanteen(Canteen canteen);

    void DeleteCanteen(Canteen canteen);

    void AddCanteen(Canteen canteen);

}