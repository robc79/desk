// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Desk.Application.UseCases.AddUserAuditEntry;
using Desk.Domain.Entities;
using Desk.WebUI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        private readonly IMediator _mediator;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger, IMediator mediator)
        {
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var userId = HttpContext.UserIdentifier();
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            var auditRequest = new AddUserAuditEntryRequest(userId, "logout");
            await _mediator.Send(auditRequest);
            
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
