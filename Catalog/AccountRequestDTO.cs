using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Glory.Domain;

public class AccountRequestDTO
{
    public string Name { get; set; }
    
    [Required, EmailAddress]
    public string Email { get; set; }
    
    [Required, PasswordPropertyText]
    public string Password { get; set; }
    public string Role { get; set; }

    public AccountRequestDTO(string name = "", string email = "", string password = "", string role = "")
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }
}