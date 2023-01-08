﻿using HorasRaras.Domain.Enums;
using HorasRaras.Domain.Utils;

namespace HorasRaras.Domain.Exceptions
{
    public class InformacaoException : Exception
    {
        public InformacaoException(StatusException status, List<string> mensagens, Exception exception = null)
           : base(status.Description(), exception)
        {
            Codigo = status;
            Mensagens = mensagens;
        }

        public InformacaoException(StatusException status, string mensagem, Exception exception = null)
            : base(status.Description(), exception)
        {
            Codigo = status;
            Mensagens = new List<string> { mensagem };
        }


        public StatusException Codigo { get; }

        public List<string> Mensagens { get; }
    }

}
