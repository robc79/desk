using System.ComponentModel.DataAnnotations;
using Desk.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Items;

public class AddModel : PageModel
{
    public class FormModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

    }

    [BindProperty]
    public FormModel Form { get; set; } = new FormModel();

    public void OnGet()
    {
    }
}