using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            try
            {
                using (MailMessage mail = new MailMessage())
                {

                    mail.Body = "Test";
                    mail.Subject = "Test Mail";
                    mail.IsBodyHtml = false;
                    mail.To.Add("ToAddress");
                    mail.From = new MailAddress("FromAddress");

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = Convert.ToInt32(465),
                        EnableSsl = false,
                        Credentials = new NetworkCredential("userName","password"),
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials=false
                    };

                    smtpClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}