using CommandModel.Command;

namespace CommandModel
{
    public interface ICommandHandler<T> where T: ICommand
    {
        void Handle(T command);
    }
}
