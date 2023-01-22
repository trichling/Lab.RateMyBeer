using System;
using System.Threading.Tasks;

namespace apetito.Composition.Command
{

    public interface ICommandAttacher
    {

        bool WillAttachTo(Type commandType);
        Task AttachTo(dynamic commandModel, ICommandExecutionContext context);

    }

    public interface ICommandAttacher<TCommandModel> : ICommandAttacher
    {

        Task AttachTo(TCommandModel commandModel, ICommandExecutionContext context);

    }
}
