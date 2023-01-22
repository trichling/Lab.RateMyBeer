using System;
using System.Threading.Tasks;

namespace apetito.Composition.Command
{
    public abstract class CommandAttacherBase<TCommandModel> : ICommandAttacher<TCommandModel>
    {
        public abstract Task AttachTo(TCommandModel commandModel, ICommandExecutionContext context);

        public Task AttachTo(dynamic commandModel, ICommandExecutionContext context)
        {
            return AttachTo((TCommandModel)commandModel, context);
        }

        public bool WillAttachTo(Type commandType)
        {
            return typeof(TCommandModel).IsAssignableFrom(commandType);
        }
    }
}
