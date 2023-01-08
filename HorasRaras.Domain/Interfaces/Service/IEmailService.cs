
namespace HorasRaras.Domain.Interfaces.Service
{
    public interface IEmailService
    {
        void EnviaEmailConfirmacaoAsync(string email, string nome, Guid hashEmailConfirmacao);
        void EnviaEmailSenha(string email, string nome, string hashSenha);
        void EnviaEmailErroCadastro(string emailDestinatario, string emailNaoEncontrado);
        void EnviaEmailErroIntegracao(string emailDestinatario, string descricao, string tarefaDesc);
    }
}
