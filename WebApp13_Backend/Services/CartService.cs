using Glory.Domain;
using WebApp13_Backend.UoW;

namespace WebApp13_Backend;

public class CartService
{
    private readonly IUnitOfWork _uow;

    public CartService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IList<ProductDTO>?> GetCartProducts(Guid accountId)
    {
        var cartItems = await _uow.CartRepository.GetCartItems(accountId);
        var products = await _uow.CatalogRepository.GetAll();
        var resultProducts = new List<ProductDTO>();
        if (cartItems != null)
            foreach (var cartItem in cartItems)
            {
                var product = products.FirstOrDefault(p => p.Id == cartItem.ProductId);
                if (product is not null)
                {
                    var resultProduct = new ProductDTO(product.Id, product.Name, product.Price)
                    {
                        Quantity = cartItem.Quantity
                    };
                    resultProducts.Add(resultProduct);
                }
            }
        return resultProducts;
    }

    public async Task Add(Guid accountId, ProductDTO product)
    {
        await _uow.CartRepository.Add(accountId, product);
        await _uow.SaveChangesAsync();
    }

    public async Task Delete(Guid accountId, ProductDTO product)
    {
        await _uow.CartRepository.Delete(accountId, product);
        await _uow.SaveChangesAsync();
    }
}