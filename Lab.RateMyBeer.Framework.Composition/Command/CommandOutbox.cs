using System.Collections.Generic;

namespace apetito.Composition.Command
{
    public class CommandOutbox : ICommandOutbox
    {
        public CommandOutbox()
        {
            Outbox = new List<object>();
        }

        public List<object> Outbox { get; }

        public TMessage Message<TMessage>(int index) where TMessage : class
        {
            return Outbox[index] as TMessage;
        }
        
        public void AddToOutbox(object command)
        {
            Outbox.Add(command);
        }
    }
}