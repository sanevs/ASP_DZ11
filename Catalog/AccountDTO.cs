using System.ComponentModel.DataAnnotations;

namespace Glory.Domain;

public class AccountDTO
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    public AccountDTO(int id = 0, string name = "", string email = "", string password = "")
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}