using Core.Domain;

namespace Core.DomainServices;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    
    Product GetProductById(int id);

    void UpdateProduct(Product product);

    void RemoveProduct(Product product);

    void AddProduct(Product product);


}