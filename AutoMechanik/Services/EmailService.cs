using System.Net.Mail;

namespace AutoMechanik.Services
{
	public class EmailService
	{
		//Отправка email локально
		public static bool SendEmail(string email, string subject, string confirmLink)
		{
			try
			{
				MailMessage message = new MailMessage();
				SmtpClient smtpClient = new SmtpClient();
				message.From = new MailAddress("noreply@auto.mechanik");
				message.To.Add(email);
				message.Subject = subject;
				message.IsBodyHtml = true;
				message.Body = confirmLink;

				smtpClient.Port = 587;
				smtpClient.Host = "localhost";

				smtpClient.Send(message);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
