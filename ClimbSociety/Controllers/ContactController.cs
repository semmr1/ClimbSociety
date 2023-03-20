using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using ClimbSociety.Models;
using ClimbSociety.ViewModels;

namespace ClimbSociety.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(string chat)
        {
            ViewData["DeveloperName"] = chat;
            return View();
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    Trace.WriteLine("Created form");
        //    return View();
        //}

        // [HttpPost]
        // public IActionResult Create(Person person)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         Debug.WriteLine("Created person object");
        //         return View("Thanks", person);
        //     }
        //     return View("Index");
        // }

        [HttpPost]
        public IActionResult Test()
        {
            return View("Thanks");
        }
        
        [HttpPost]
        public IActionResult Send(Message message)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7291/api/messages");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Message>("message", message);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Thanks");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                //Debug.WriteLine("Created email");
                //return View("Thanks", message);
            }
            return View();
        }
    }
}
