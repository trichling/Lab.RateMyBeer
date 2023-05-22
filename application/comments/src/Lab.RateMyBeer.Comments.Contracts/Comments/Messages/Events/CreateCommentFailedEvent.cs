namespace Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Events
{
    public class CreateCommentFailedEvent
    {
        public Guid CommentId { get; set; }
        public Guid CheckinId { get; set; }
    }
}