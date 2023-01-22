using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apetito.Composition.Command
{
    public class CommandExecutionContext : ICommandExecutionContext
    {

        private readonly Dictionary<string, object> values;
        private readonly List<ICommandAttacher> attacher;
        private readonly ICommandOutbox sender;

        public CommandExecutionContext(ICommandOutbox sender)
            : this(sender, new List<ICommandAttacher>())
        {
        }

        public CommandExecutionContext(ICommandOutbox sender, IEnumerable<ICommandAttacher> attacher)
        {
            this.values = new Dictionary<string, object>();
            this.attacher = attacher.ToList();
            this.sender = sender;
        }

        public ICommandExecutionContext AddAttacher<TCommandViewModel>(ICommandAttacher<TCommandViewModel> attacher)
        {                        
            this.attacher.Add(attacher);
            return this;
        }

        public T GetValue<T>(string key)
        {
            if (!values.ContainsKey(key))
                return default(T);
            return (T) values[key];
        }

        public ICommandExecutionContext SetValue<T>(string key, T value)
        {
            if(!values.ContainsKey(key))
                values.Add(key, value);
            values[key] = value;
            return this;
        }

        public async Task<ICommandOutbox> BuildCommands<TCommandViewModel>(TCommandViewModel commandViewModel)
        {
            var attachers = AttacherFor<TCommandViewModel>();
            foreach (var attacher in attachers)
            {
                await AddToOutbox(attacher.AttachTo(commandViewModel, this));
            }

            return sender;
        }

        public async Task AddToOutbox(object command)
        {
            sender.AddToOutbox(command);
        }

        public IEnumerable<ICommandAttacher<TCommandViewModel>> AttacherFor<TCommandViewModel>()
        {
            // TODO: dictionary<type, list<IViewModelAppender>>
            return attacher.Where(a => a.WillAttachTo(typeof(TCommandViewModel)))
                .Cast<ICommandAttacher<TCommandViewModel>>();
        }

    }
}
