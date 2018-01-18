namespace LogParser.CLI.CommandHandlers
{
    public interface IHandler
    {
        void Handle(params object[] args);
    }
}
