using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages
{
    public class SwCreateModel : PageModel
    {
        private readonly SandwitchService _service;

        public SwCreateModel(SandwitchService service)
        {
            _service = service;
        }

        [BindProperty]
        public SandwitchModel NewSandwitch { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdSandwitch = await _service.AddSandwitch(NewSandwitch);

            if (createdSandwitch == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create sandwich.");
                return Page();
            }

            return RedirectToPage("SwList"); // Redirect to list page after creation
        }
    }
}
