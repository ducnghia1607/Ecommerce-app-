using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "The password must have 1 uppercase , 1 lowercase , 1 number, 1 non-alphanumeric , at least 6 character ")]
    public string Password { get; set; }
    public string DisplayName { get; set; }
}
