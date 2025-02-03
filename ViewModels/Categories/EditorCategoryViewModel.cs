using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels.Categories
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "Nao deve ser vazio")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Necessario entre 3 e 40 caracteres")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
    }
}
