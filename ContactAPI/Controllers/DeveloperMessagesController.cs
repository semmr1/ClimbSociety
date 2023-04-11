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
using MimeKit;
using MailKit.Net.Smtp;

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

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeveloperMessage>> PostMessage(DeveloperMessage message)
        {
            if(ModelState.IsValid)
            {
                SendMail(message);
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        private static void SendMail(DeveloperMessage devMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("HOST_NAME"), Environment.GetEnvironmentVariable("HOST_EMAIL")));
            message.To.Add(new MailboxAddress(Environment.GetEnvironmentVariable("HOST_NAME"), Environment.GetEnvironmentVariable("HOST_EMAIL")));
            message.Subject = devMessage.Subject;

            message.Body = new TextPart("plain")
            {
                Text = $"FROM: { devMessage.Email } \n\n" +
                $"{devMessage.MessageText}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp-mail.outlook.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(Environment.GetEnvironmentVariable("HOST_EMAIL"), Environment.GetEnvironmentVariable("HOST_PASS"));

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
