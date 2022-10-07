using Domain;

namespace Core.DomainServices;

public interface ICanteenRepository
{
    IEnumerable<Canteen> GetCanteens();

    Canteen GetCanteenById();

}