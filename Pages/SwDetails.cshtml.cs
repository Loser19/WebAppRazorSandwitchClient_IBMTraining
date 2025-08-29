using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages
{
    public class SwDetailsModel : PageModel
    {
        private readonly SandwitchService _service;

        public SwDetailsModel(SandwitchService service)
        {
            _service = service;
        }

        public SandwitchModel SandwitchDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var sandwich = await _service.GetSandwitchByIdAsync(id);
            if (sandwich == null)
            {
                return NotFound();
            }

            SandwitchDetails = sandwich;
            return Page();
        }
    }
}
