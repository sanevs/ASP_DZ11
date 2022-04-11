namespace Glory.Domain;

public interface IEmailSender
{
    Task<string>? SendAsync(string text, CancellationToken cancellationToken = default);
}