using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RazorEngine;

namespace LicentaApp.Domain.Email
{
    public static class EmailService
    {
        public static async Task SendEmail(ControllerContext context, EmailDefinition definition, object model, string to, string[] cc = null)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            if (cc != null)
            {
                cc.ToList().ForEach(x =>
                {
                    message.CC.Add(new MailAddress(x));
                });
            }
            message.Subject = definition.Subject;
            if (context == null)
            {
                message.Body = Razor.Parse(System.IO.File.ReadAllText(definition.Body), model);
            }
            else
            {
                if (definition.Attachment != null)
                {
                    var pdf = PrintService.CreateAttachment(definition.Attachment, model);
                    MemoryStream ms = new MemoryStream(pdf.BuildPdf(context));
                    message.Attachments.Add(new Attachment(ms, definition.AttachmentName));
                }
                message.Body = PrintService.RenderViewToString(context, definition.Body, model, true);

            }


            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}