using Domain;

namespace Core.DomainServices;

public interface ProductRepository
{
    IEnumerable<Product> GetProducts();
    
    Product GetProductById(int id);
    
    
}