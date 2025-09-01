using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages
{
    public class SwListModel : PageModel
    {
        private readonly SandwitchService _service;

        public SwListModel(SandwitchService service)
        {
            _service = service;
        }

        public Task<List<SandwitchModel>> SwList { get; set; } = default!;

        public IActionResult OnGet()
        {
            // ✅ Redirect to login if not logged in
            var isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (string.IsNullOrEmpty(isLoggedIn) || isLoggedIn != "true")
            {
                return RedirectToPage("/Account/Login");
            }

            SwList = _service.GetSandwiches();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (string.IsNullOrEmpty(isLoggedIn) || isLoggedIn != "true")
            {
                return RedirectToPage("/Account/Login");
            }

            var success = await _service.DeleteSandwitchAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete sandwich.");
            }

            return RedirectToPage(); // Refresh the list
        }
    }
}
