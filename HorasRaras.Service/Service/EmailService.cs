using System.Net.Mail;
using System.Net;
using HorasRaras.Domain.Contracts.Settings;
using HorasRaras.Domain.Interfaces.Service;


namespace HorasRaras.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        //Email service poderia ser feito usando injecao de dependencia, criando uma interface para o
        //smtp client, do jeito que esta aqui não foi possível fazer o mock do smtp
        //é uma melhoria a se fazer na aplicação e assim possibilitar o teste unitário desta classe
        public void EnviaEmailConfirmacaoAsync(string emailDestinatario, string nome, Guid hashEmailConfirmacao)
        {
            string template = File.ReadAllText(@"../HorasRaras.Domain/Templates/templateConfirmarEmail.html");
            string templateUpdate = template.Replace("##link##", $"https://localhost:7233/conta/email/hash={hashEmailConfirmacao}").Replace("##nome##", nome);

            var smtpClient = new SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Username), // quem está enviando
                Subject = "Horas Raras",
                Body = templateUpdate,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailDestinatario);
            
            smtpClient.Send(mailMessage);
                
        }
        public void EnviaEmailSenha(string emailDestinatario, string nome, string hashSenhaConfirmacao)
        {
            string template = File.ReadAllText(@"../HorasRaras.Domain/Templates/templateResetSenha.html");
            string templateUpdate = template.Replace("##hash##", $"{hashSenhaConfirmacao}").Replace("##nome##",nome);

            var smtpClient = new SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Username), // quem está enviando
                Subject = "Horas Raras",
                Body = templateUpdate,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailDestinatario);

            smtpClient.Send(mailMessage);

        }
        public void EnviaEmailErroCadastro(string emailDestinatario, string emailNaoEncontrado)
        {
            string template = File.ReadAllText(@"../HorasRaras.Domain/Templates/templateErroCadastro.html");
            string templateUpdate = template.Replace("##email##", emailNaoEncontrado);

            var smtpClient = new SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Username), // quem está enviando
                Subject = "Horas Raras",
                Body = templateUpdate,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailDestinatario);

            smtpClient.Send(mailMessage);

        }
        public void EnviaEmailErroIntegracao(string emailDestinatario, string descricao, string tarefaDesc)
        {
            string template = File.ReadAllText(@"../HorasRaras.Domain/Templates/templateErroIntegracao.html");
            string templateUpdate = template.Replace("##descricao##", descricao).Replace("##tarefa##", tarefaDesc);

            var smtpClient = new SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Username), // quem está enviando
                Subject = "Horas Raras",
                Body = templateUpdate,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailDestinatario);

            smtpClient.Send(mailMessage);

        }
    }
}

