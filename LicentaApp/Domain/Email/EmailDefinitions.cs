using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.Email
{
    public class EmailDefinitions
    {
        public static EmailDefinition CerereAprovizionareFurnizor = new EmailDefinition
        {
            Id = "ac9cd024-28fd-4dc0-a9c2-4098b085f1a3",
            Name = "Cerere Aprovizionare",
            Attachment = "~/Views/EmailTemplates/CerereAprovizionareFurnizorAttachment.cshtml",
            AttachmentName = "Cerere-Aprovizionare.pdf",
            Body = "~/Views/EmailTemplates/CerereAprovizionareFurnizor.cshtml",
            Subject = "Cerere Aprovizionare"
        };
    }
}