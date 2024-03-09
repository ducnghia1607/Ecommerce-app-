using System.ComponentModel.DataAnnotations;

namespace Core;

public class Address
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    // Bởi vì IdentityUser có Id là kiểu string nên khi derive thì AppUser id cũng có kiểu string 
    // Vì Aspnetcore khởi tạo string là null nên cái này có thể nullable nên phải có required 
    [Required]
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
