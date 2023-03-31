using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using ClimbSociety.Models;
using ClimbSociety.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ClimbSociety.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult DeveloperProfile()
        {
            Developer dev = new()
            {
                Id = 1,
                Name = "David Vulkers",
                Description = "Als AI specialist ben ik bekwaam in het onderzoeken van zowel de ethische en technische kant van het development proces",
                Skills = new Dictionary<string, int> {
                    {"Ethiek", 65},
                    {"Java", 45},
                    {"C#", 90}
                }
            };
            return View(new DeveloperProfileViewModel(dev));
        }
    }
}
