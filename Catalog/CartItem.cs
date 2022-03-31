namespace Glory.Domain;

public class CartItem
{
    public Guid Id { get; init; }
    public Guid CartId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }

    public CartItem(Guid cartId, int productId, decimal quantity = 1)
    {
        Id = Guid.NewGuid();
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }
}