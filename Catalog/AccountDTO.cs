using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace Glory.Domain;

public class AccountDTO
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required, EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string HashedPassword { get; set; }

    public AccountDTO(int id = 0, string name = "", string email = "", string hashedPassword = "")
    {
        Id = id;
        Name = name;
        Email = email;
        HashedPassword = hashedPassword;
    }
}