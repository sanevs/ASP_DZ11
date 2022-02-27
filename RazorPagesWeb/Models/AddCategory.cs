using System.ComponentModel.DataAnnotations;
using System.Linq;
using Glory.Domain;

namespace RazorPagesWeb.Models;

public class AddCategory
{
    [Range(1, 3)]
    public int Id { get; }
    public string Name { get; }
}