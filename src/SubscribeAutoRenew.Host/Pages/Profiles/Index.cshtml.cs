using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SubscribeAutoRenew.Host.Models;
using SubscribeAutoRenew.Host.Service;

namespace SubscribeAutoRenew.Host.Pages.Profiles
{
    public class IndexModel : PageModel
    {
        public IEnumerable<ProfileModel> Profiles { get; set; } = default!;
        private readonly IProfileService _profileService;
        public IndexModel(IProfileService profileService)
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Profiles = await _profileService.GetProfiles();
            return Page();
        }
    }
}
