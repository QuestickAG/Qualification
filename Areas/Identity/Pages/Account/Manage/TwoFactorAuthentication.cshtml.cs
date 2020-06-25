using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Qualification.Models;

namespace Qualification.Areas.Identity.Pages.Account.Manage
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DbContext DbContext { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public TwoFactorAuthenticationModel(
            UserManager<IdentityUser> userManager,
            DbContext context)
        {
            _userManager = userManager;
            DbContext = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Не указано имя")]
            [MinLength(3)]
            [Display(Name = "Имя")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Не указана фамилия")]
            [MinLength(3)]
            [Display(Name = "Фамилия")]
            public string Surname { get; set; }

            [MinLength(3)]
            [Display(Name = "Отчество")]
            public string MiddleName { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var profile = await DbContext.Set<ProfileInfo>().FirstOrDefaultAsync(x => x.UserId == user.Id);

            Input = new InputModel
            {
                Name = profile?.Name ?? "",
                Surname = profile?.SurName ?? "",
                MiddleName = profile?.MiddleName ?? ""
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var profile = await DbContext.Set<ProfileInfo>().FirstOrDefaultAsync(x => x.UserId == user.Id);

                if(profile == null)
                {
                    var newProfile = new ProfileInfo() 
                    {
                        UserId = user.Id, 
                        MiddleName = "", 
                        SurName = "", 
                        Name = ""
                    };
                    DbContext.Add(newProfile);
                    await DbContext.SaveChangesAsync();
                    profile = newProfile;
                }

                profile.Name = Input.Name;
                profile.SurName = Input.Surname;
                profile.MiddleName = Input.MiddleName;

                await DbContext.SaveChangesAsync();
            }

            StatusMessage = "Анкета обновлена";
            return RedirectToPage();
        }
    }
}