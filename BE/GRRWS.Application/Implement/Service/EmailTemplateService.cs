using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.EmailTemplate;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.Extensions.Configuration;

namespace GRRWS.Application.Implement.Service
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepsository;
        private readonly IFirebaseService _firebaseService;
        private readonly IConfiguration _configuration;

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepsository, IConfiguration configuration, IFirebaseService firebaseService)
        {
            _emailTemplateRepsository = emailTemplateRepsository;
            _configuration = configuration;
            _firebaseService = firebaseService;
        }

        public async Task<string> GenerateEmailBody(string emailTemplateType, Dictionary<string, string> placeholders)
        {
            var emailTemplate = await _emailTemplateRepsository.GetEmailTemplateByTypeAsync(emailTemplateType);
            if (emailTemplate == null)
                throw new Exception("Email template not found");

            string body = emailTemplate.Body;
            foreach (var placeholder in placeholders)
            {
                body = body.Replace($"{{{placeholder.Key}}}", placeholder.Value);
            }

            return body;
        }

        public async Task<dynamic> SendMail(MailObject mailObject)
        {
            try
            {
                string smtpServer = _configuration["SMTP:Server"];
                int port = int.Parse(_configuration["SMTP:Port"]);
                string senderEmail = _configuration["MailService:MailSender"];
                string password = _configuration["MailService:PasswordSender"];
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mailObject.ToMailIds.ForEach(x =>
                    {
                        mail.To.Add(x);
                    }
                    );
                    mail.Subject = mailObject.Subject;
                    mail.Body = mailObject.Body;
                    mail.IsBodyHtml = mailObject.IsBodyHtml;
                    mailObject.Attachments.ForEach(x =>
                    {
                        mail.Attachments.Add(new Attachment(x));
                    });
                    using (SmtpClient smtp = new SmtpClient(smtpServer, port))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(senderEmail, password);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return Result.Success();
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        public async Task<Result> SaveEmailTemplateAsync(EmailTemplate emailTemplate)
        {
            try
            {
                await _emailTemplateRepsository.AddEmailTemplateAsync(emailTemplate); // Use the new Add method
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("SaveError", $"Failed to save email template: {ex.Message}"));
            }
        }
        public async Task<Result> GetTemplateByTypeAsync(string templateType)
        {
            var emailTemplate = await _emailTemplateRepsository.GetEmailTemplateByTypeAsync(templateType);
            if (emailTemplate == null)
            {
                return Result.Failure(Error.Failure("TemplateNotFound", $"Template of type '{templateType}' not found."));
            }

            return Result.SuccessWithObject(emailTemplate);
        }

        public async Task<Result> GenerateEmailWithActivationLink(string templateType, string activationLink, Dictionary<string, string> additionalPlaceholders = null)
        {
            // Retrieve the email template by type
            var templateResult = await GetTemplateByTypeAsync(templateType);
            if (templateResult.IsFailure)
            {
                return Result.Failure(Error.Failure("TemplateNotFound", "Email template not found"));
            }

            var emailTemplate = templateResult.Object as EmailTemplate;
            var htmlContent = emailTemplate.Body;

            // Deserialize image mappings JSON
            var imageMappings = JsonSerializer.Deserialize<Dictionary<string, string>>(emailTemplate.ImageMappingsJson);

            // Replace image placeholders with actual URLs from Firebase
            foreach (var mapping in imageMappings)
            {
                var getImageRequest = new GetImageRequest(mapping.Value); // Firebase path
                var imageUrlResult = await _firebaseService.GetImageAsync(getImageRequest);
                if (imageUrlResult.IsSuccess)
                {
                    var imageUrl = ((GetImageResponse)imageUrlResult.Object).ImageUrl;
                    htmlContent = htmlContent.Replace($"{{{mapping.Key}}}", imageUrl); // Replace placeholder with actual URL
                }
            }

            // Replace {VerifyURL} placeholder with the activation link
            htmlContent = htmlContent.Replace("{VerifyURL}", activationLink);

            // Replace any additional placeholders if provided
            if (additionalPlaceholders != null)
            {
                foreach (var placeholder in additionalPlaceholders)
                {
                    htmlContent = htmlContent.Replace($"{{{placeholder.Key}}}", placeholder.Value);
                }
            }

            return Result.SuccessWithObject(htmlContent);
        }
        public async Task<Result> GenerateEmailWithAppointmentLink(string templateType, string appointmentDetailUrl, Dictionary<string, string> additionalPlaceholders = null)
        {
            // Retrieve the email template by type
            var templateResult = await GetTemplateByTypeAsync(templateType);
            if (templateResult.IsFailure)
            {
                return Result.Failure(Error.Failure("TemplateNotFound", "Email template not found"));
            }

            var emailTemplate = templateResult.Object as EmailTemplate;
            var htmlContent = emailTemplate.Body;

            // Deserialize image mappings JSON
            var imageMappings = JsonSerializer.Deserialize<Dictionary<string, string>>(emailTemplate.ImageMappingsJson);

            // Replace image placeholders with actual URLs from Firebase
            foreach (var mapping in imageMappings)
            {
                var getImageRequest = new GetImageRequest(mapping.Value); // Firebase path
                var imageUrlResult = await _firebaseService.GetImageAsync(getImageRequest);
                if (imageUrlResult.IsSuccess)
                {
                    var imageUrl = ((GetImageResponse)imageUrlResult.Object).ImageUrl;
                    htmlContent = htmlContent.Replace($"{{{mapping.Key}}}", imageUrl); // Replace placeholder with actual URL
                }
            }

            // Replace {AppointmentDetailURL} placeholder with the appointment detail link
            htmlContent = htmlContent.Replace("{AppointmentDetailURL}", appointmentDetailUrl);

            // Replace any additional placeholders if provided
            if (additionalPlaceholders != null)
            {
                foreach (var placeholder in additionalPlaceholders)
                {
                    htmlContent = htmlContent.Replace($"{{{placeholder.Key}}}", placeholder.Value);
                }
            }

            return Result.SuccessWithObject(htmlContent);
        }
        public async Task<Result> GenerateEmailForAppointmentStatusAsync(string templateType, string status, string appointmentDetailUrl, Dictionary<string, string> placeholders)
        {
            // Retrieve the email template by type
            var templateResult = await GetTemplateByTypeAsync(templateType);
            if (templateResult.IsFailure)
            {
                return Result.Failure(Error.Failure("TemplateNotFound", "Email template not found"));
            }

            var emailTemplate = templateResult.Object as EmailTemplate;
            var htmlContent = emailTemplate.Body;

            // Replace placeholders like {AppointmentDate}, {AppointmentTime}, etc.
            foreach (var placeholder in placeholders)
            {
                htmlContent = htmlContent.Replace($"{{{placeholder.Key}}}", placeholder.Value);
            }

            // Replace the status-specific placeholders if any, and the Appointment Detail URL
            htmlContent = htmlContent.Replace("{Status}", status);
            htmlContent = htmlContent.Replace("{AppointmentDetailURL}", appointmentDetailUrl);

            return Result.SuccessWithObject(htmlContent);
        }
    }
}
