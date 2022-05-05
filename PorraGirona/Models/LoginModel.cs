using System.ComponentModel.DataAnnotations;

namespace PorraGirona.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Escriu el alias d’usuari")]
        [Display(Name = "alias")]
        public string Alias { get; set; }
        [Required(ErrorMessage = "Escriu el password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

