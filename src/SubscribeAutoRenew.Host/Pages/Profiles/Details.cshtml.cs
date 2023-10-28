using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SubscribeAutoRenew.Host.Models;
using SubscribeAutoRenew.Host.Service;

namespace SubscribeAutoRenew.Host.Pages.Profiles
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public ProfileModel Profile { get; set; } = new ProfileModel();
        [BindProperty]
        public bool IsRefreshed { get; set; }
        private readonly IProfileService _profileService;
        public DetailsModel(IProfileService profileService)
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _profileService.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            else
            {
                IsRefreshed = TempData["Refreshed"]?.ToString() == bool.TrueString;
                Profile = profile;
            }
            return Page();
        }
    }
}
