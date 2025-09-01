using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages
{
    public class SwListModel : PageModel
    {
        private SandwitchService? _service;

        public SwListModel(SandwitchService service)
        {
            _service = service;
        }

        public Task<List<SandwitchModel>> SwList { get; set; } = default!;

        public void OnGet()
        {
            SwList = _service.GetSandwiches();

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _service.DeleteSandwitchAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete sandwich.");
            }

            return RedirectToPage(); // Refresh the list
        }
    }
}
