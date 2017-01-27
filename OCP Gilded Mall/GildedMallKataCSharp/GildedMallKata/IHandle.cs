namespace GildMallKata
{
    public interface IHandle<in TCommand, out TResult> where TCommand : Command
    {
        TResult Handle(TCommand command);
    }
}