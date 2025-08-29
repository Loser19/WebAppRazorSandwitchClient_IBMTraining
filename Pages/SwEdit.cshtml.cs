using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages
{
    public class SwEditModel : PageModel
    {
        private readonly SandwitchService _service;

        public SwEditModel(SandwitchService service)
        {
            _service = service;
        }

        [BindProperty]
        public SandwitchModel EditSandwitch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var sandwich = await _service.GetSandwitchByIdAsync(id);
            if (sandwich == null)
            {
                return NotFound();
            }

            EditSandwitch = sandwich;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var updated = await _service.UpdateSandwitchAsync(EditSandwitch);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Update failed.");
                return Page();
            }

            return RedirectToPage("SwList");
        }
    }
}
