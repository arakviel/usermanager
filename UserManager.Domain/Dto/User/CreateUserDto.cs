using System.ComponentModel.DataAnnotations;

namespace UserManager.Domain.Dto.User;

public class CreateUserDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [Range(0, 120, ErrorMessage = "Age must be between 0 and 120")]
    public int Age { get; set; }
}
