using System.Collections.Generic;
using System.Threading.Tasks;

namespace apetito.Composition.Command
{
    public interface ICommandExecutionContext
    {

        Task AddToOutbox(object command); 

        Task<ICommandOutbox> BuildCommands<TCommandViewModel>(TCommandViewModel commandViewModel);

        ICommandExecutionContext AddAttacher<TCommandViewModel>(ICommandAttacher<TCommandViewModel> attacher);

        T GetValue<T>(string key);

        ICommandExecutionContext SetValue<T>(string key, T value);
        IEnumerable<ICommandAttacher<TCommandViewModel>> AttacherFor<TCommandViewModel>();
    }
}
