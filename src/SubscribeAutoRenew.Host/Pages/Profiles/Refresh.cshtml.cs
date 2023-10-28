using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SubscribeAutoRenew.Host.Service;

namespace SubscribeAutoRenew.Host.Pages.Profiles
{
    public class RefreshModel : PageModel
    {
        private readonly IProfileService _profileService;
        public RefreshModel(IProfileService profileService)
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            await _profileService.RenewProfile(id);
            TempData["Refreshed"] = true;
            return RedirectToPage("Details", new { id });
        }
    }
}
