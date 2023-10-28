using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SubscribeAutoRenew.Host.Models;
using SubscribeAutoRenew.Host.Service;

namespace SubscribeAutoRenew.Host.Pages.Profiles
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ProfileModel Profile { get; set; } = new ProfileModel();
        private readonly IProfileService _profileService;
        public EditModel(IProfileService profileService)
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id != null)
                Profile = await _profileService.GetById(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Profile.Id == null)
            {
                await _profileService.AddProfile(Profile);
            }
            else
            {
                await _profileService.UpdateProfile(Profile);
            }

            return RedirectToPage("./Index");
        }
    }
}
