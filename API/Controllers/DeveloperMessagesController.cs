using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using API.Model;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperMessagesController : ControllerBase
    {
        private readonly APIContext _context;

        public DeveloperMessagesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperMessage>>> GetMessage()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperMessage>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutMessage(int id, DeveloperMessage message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }   

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<DeveloperMessage>> PostMessage(DeveloperMessage message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }

        //private async void SendMail()
        //{
        //    using (var http = new HttpClient())
        //    {
        //        http.DefaultRequestHeaders.Authorization =
        //             new AuthenticationHeaderValue("Basic", mailchimpapikey - us1);
        //        string content = await http.GetStringAsync(@"https://us1.api.mailchimp.com/3.0/lists");
        //        Console.WriteLine(content);
        //    }
        //}

        //public static void SendMail()
        //{
        //    GetEnvPath();
        //    Execute().Wait();
        //}

        //private static void GetEnvPath()
        //{
        //    string path = "";

        //    //Check OS platform enum and converted to an int
        //    int p = (int)Environment.OSVersion.Platform;

        //    //Check if enum is a 4 or 128 that are Unix / Mac enums, other enums are for Windows

        //    //MacOS or Linux
        //    if ((p == 4) || (p == 128))
        //    {
        //        path += Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))))))));
        //    }
        //    //Windows
        //    else
        //    {
        //        path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))));
        //    }

        //    //Debug.WriteLine(path);
        //    var dotenv = Path.Combine(path, ".env");
        //    Load(dotenv);
        //}

        //private static void Load(string filePath)
        //{
        //    if (!System.IO.File.Exists(filePath))
        //        return;

        //    foreach (var line in System.IO.File.ReadAllLines(filePath))
        //    {
        //        var parts = line.Split(
        //            '=',
        //            StringSplitOptions.RemoveEmptyEntries);

        //        if (parts.Length != 2)
        //            continue;

        //        Environment.SetEnvironmentVariable(parts[0], parts[1]);
        //    }
        //}

        //private static async Task Execute()
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("API_KEY");
        //    var client = new SendGridClient(apiKey);
        //    var msg = new SendGridMessage()
        //    {
        //        From = new EmailAddress("semroangroenendal@gmail.com", "Sem Groenendal"),
        //        Subject = "C# email",
        //        PlainTextContent = "Test email send from c# code",
        //        HtmlContent = "Test email send from c# code"
        //    };
        //    msg.AddTo(new EmailAddress("s.groenendal01@gmail.com", "Sem Groenendal"));
        //    var response = await client.SendEmailAsync(msg);

        //    // A success status code means SendGrid received the email request and will process it.
        //    // Errors can still occur when SendGrid tries to send the email. 
        //    // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
        //    Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
        //}

        //public void Send()
        //{
        //    SendMail();
        //}
    }
}
