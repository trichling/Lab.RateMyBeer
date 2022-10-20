namespace Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Events
{
    public class CreateCommentSucceededEvent
    {
        public Guid CommentId { get; set; }
        public Guid CheckinId { get; set; }
    }
}