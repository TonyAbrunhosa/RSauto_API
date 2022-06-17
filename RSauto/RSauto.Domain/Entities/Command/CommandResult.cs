﻿using RSauto.Domain.Contracts.Command;

namespace RSauto.Domain.Entities.Command
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool sucesso, string mensagem, object dados = null)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
    }
}
