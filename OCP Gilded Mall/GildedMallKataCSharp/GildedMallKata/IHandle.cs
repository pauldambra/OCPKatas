namespace GildMallKata
{
    public interface IHandle<in TCommand, out TResult>
    {
        TResult Handle(TCommand command);
    }
}