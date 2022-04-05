namespace Glory.Domain;

public class TwoFA
{
    public Guid Id { get; init; }
    public Guid AccountId { get; set; }
    public int Code { get; set; }

    public TwoFA(Guid id, Guid accountId, int code)
    {
        Id = id;
        AccountId = accountId;
        Code = code;
    }
}