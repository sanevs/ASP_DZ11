using Glory.Domain;

namespace BlazorWebAssemblyKW12.Data;

public class Cart : ICart
{
    private readonly IDictionary<Product, int> _productsCart = new Dictionary<Product, int>();
    public void AddProduct(Product product)
    {
        int quantity = 0;
        KeyValuePair<Product, int> kvCartProduct;
        try
        {
            kvCartProduct = _productsCart.First(p => p.Key == product);
            quantity = kvCartProduct.Value;
        }
        catch (Exception e)
        {
            _productsCart.Add(product, 1);
            return;
        }
        if (_productsCart.Remove(kvCartProduct.Key))
        {
            _productsCart.Add(product, quantity + 1);
        }
        
        // if (!_productsCart.TryAdd(product, 1))
        // {
        //     _productsCart.TryGetValue(product, out int quantity);
        //     _productsCart.Remove(product);
        //     _productsCart.Add(product, quantity + 1);
        // }
    }
    public void DeleteProduct(Product product) => _productsCart.Remove(product);
    public void Clear() => _productsCart.Clear();
    public IDictionary<Product, int> GetCartProducts() => _productsCart;
    public int GetCount() => _productsCart.Count;
}