namespace Lab.RateMyBeer.Framework.Composition.Command
{
    public interface ICommandOutbox
    {

        List<object> Outbox { get; }

        void AddToOutbox(object command);

    }
}
