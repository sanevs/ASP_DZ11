namespace Glory.Domain;

public class Cart
{
    public Guid Id { get; init; }
    public Guid AccountId { get; set; }
    //public List<ProductDTO> Items { get; set; }

    public Cart(Guid accountId)
    {
        Id = new Guid();
        AccountId = accountId;
        //Items = new List<ProductDTO>();
    }
}