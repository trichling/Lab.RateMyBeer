using System.Collections.Generic;

namespace apetito.Composition.Command
{
    public interface ICommandOutbox
    {

        List<object> Outbox { get; }

        void AddToOutbox(object command);

    }
}
