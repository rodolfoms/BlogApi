using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "nome eh obrigatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "informe o email")]
        [EmailAddress(ErrorMessage = "Email eh obrigatorio")]
        public string Email { get; set; }
    }
}
