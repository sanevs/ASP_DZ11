using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace Glory.Domain;

public class AccountDTO
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required, EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string HashedPassword { get; set; }
    public string Role { get; set; }
    public bool IsBanned { get; set; }

    public AccountDTO(Guid id, bool isBanned = false, string name = "", string email = "", string hashedPassword = "", string role = "")
    {
        Id = id;
        Name = name;
        Email = email;
        HashedPassword = hashedPassword;
        Role = role;
        IsBanned = isBanned;
    }
}