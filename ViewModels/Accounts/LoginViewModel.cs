using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels.Accounts
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "informe o email")]
        [EmailAddress(ErrorMessage = "email eh obrigatorio")]
        public string Email { get; set; }


        [Required(ErrorMessage = "senha eh obrigatorio")]
        public string Password { get; set; }
    }
}
