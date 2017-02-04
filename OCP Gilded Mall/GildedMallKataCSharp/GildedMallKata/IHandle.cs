using System.Threading.Tasks;

namespace GildMallKata
{
    public interface IHandle<in TCommand> where TCommand : Command
    {
        Task Handle(TCommand command);
    }
}