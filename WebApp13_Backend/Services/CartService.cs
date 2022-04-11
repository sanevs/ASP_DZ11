using System.Reflection.Metadata.Ecma335;
using Glory.Domain;
using WebApp13_Backend.UoW;

namespace WebApp13_Backend;

public class CartService
{
    private readonly IUnitOfWork _uow;
    private readonly IEmailSender _email;

    public CartService(IUnitOfWork uow, IEmailSender email)
    {
        _uow = uow;
        _email = email;
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

    public async Task<string?> SendOrderByEmail(AccountDTO account)
    {
        var products = await GetCartProducts(account.Id);
        string text = "Hi " + account.Name + ", your order:\n";
        foreach (var product in products)
            text += product.Name + ", " + product.Price + "$, кол-во " + product.Quantity + "\n";
        return await _email.SendAsync(text);
    }

    public async Task Clear(AccountDTO account)
    {
        await _uow.CartRepository.Clear(account.Id);
        await _uow.SaveChangesAsync();
    }
}