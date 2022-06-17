namespace RSauto.Domain.Contracts.Command
{
    public interface ICommandResult
    {
        bool Sucesso { get; set; }
        string Mensagem { get; set; }
        object Dados { get; set; }
    }
}
