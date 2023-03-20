using Microsoft.AspNetCore.Mvc;
using ClimbSociety.Models;
using ClimbSociety.ViewModels;

namespace ClimbSociety.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            Developer dev = new()
            {
                Id = 1,
                Name = "David Vulkers",
                Description = "Als AI specialist ben ik bekwaam in het onderzoeken van zowel de ethische en technische kant van het development proces",
                Skills = new Dictionary<string, int> {
                    {"Ethiek", 75},
                    {"Java", 60},
                    {"C#", 75}
                }
            };
            return View(new ProfileViewModel(dev));
        }
    }
}
