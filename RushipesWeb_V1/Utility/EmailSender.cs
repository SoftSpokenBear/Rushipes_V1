using Microsoft.AspNetCore.Identity.UI.Services;

namespace RushipesWeb_V1.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            return Task.CompletedTask;
        }
    }
}
