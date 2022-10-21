using Core.DomainServices;
using Domain;

namespace Infrastructure;

public class ProductEFRepository : IProductRepository
{
    private ApplicationDBContext _context;

    public ProductEFRepository(ApplicationDBContext context)
    {
        _context = context;
    }


    public IEnumerable<Product> GetProducts()
    {
        return _context.Products.ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products.First(product => product.Id == id );
    }

    public void UpdateProduct(Product product)
    {
        _context.Update(product);
        _context.SaveChanges();
    }

    public void RemoveProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
}