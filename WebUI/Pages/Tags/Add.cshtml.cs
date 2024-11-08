using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Desk.Application.UseCases.AddUserTag;
using Desk.Shared;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desk.WebUI.Pages.Tags;

public class AddModel : PageModel
{
    public class FormModel
    {
        [Required]
        [MaxLength(Constants.MaxTagLength)]
        public string Name { get; set; } = string.Empty;
    }
    
    private readonly IMediator _mediator;

    public AddModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); 
    }

    [BindProperty]
    public FormModel Form { get; set; } = new FormModel();

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var userId = HttpContext.UserIdentifier();
        var request = new AddUserTagRequest(userId, Form.Name);
        var response = await _mediator.Send(request);
        
        if (response.Error is not null)
        {
            return StatusCode(500);
        }

        return RedirectToPage("/Tags/List");
    }
}