using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FUForum.ViewModels.Systems;

public class CommandVM
{
    public string Id { get; set; }
    
    public string Name { get; set; }
}