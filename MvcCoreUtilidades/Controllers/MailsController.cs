using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration configuration;

        public MailsController(IConfiguration configuration)
        {
            this.configuration=configuration;

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(string to,string asunto,string mensaje)
        {
            string user=this.configuration.GetValue<string>("MailSettings:Credentials:User");
            //CREAMOS UN OBJETO PARA LA INFORMACIÓN DEL MAIL
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;
            //<h1>Holaa</h1>
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            //RECUPERAMOS LOS DATOS PARA EL OBJETO QUE MANDA EL PROPIO MAIL
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("Server:Host");
            int port = this.configuration.GetValue<int>("Server:Port");
            bool ssl = this.configuration.GetValue<bool>("Server:Ssl");
            bool defualtCredentials = this.configuration.GetValue<bool>("Server:DefualtCredentials");

            //CREAMOS EL OBJETO SMTPCLIENT PARA ENVIAR LOS DATOS
            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = port;
            client.EnableSsl = ssl;
            client.UseDefaultCredentials = defualtCredentials;
            //CREDENCIALES PARA EL MAIL
            NetworkCredential credentials = new NetworkCredential(user, password);
            client.Credentials = credentials;//metemos las credenciales
            await client.SendMailAsync(mail);
            ViewData["MENSAJE"] = "MENSJAE ENVIADO CORRECTAMENTE!!";

            return View();
        }


    }
}
