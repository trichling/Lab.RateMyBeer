namespace Lab.RateMyBeer.Comments.Data.Comments;

public class CommentsData
{
    public CommentsData()
    {
        Comments = new List<CommentData>();
    }
    public CommentsData(Guid commentsId, Guid checkinId, List<CommentData> comments)
    {
        this.CommentsId = commentsId;
        this.CheckinId = checkinId;
        this.Comments = comments;
    }

    public Guid CommentsId { get; set; }
    public Guid CheckinId { get; set; }

    public string UserComment { get; set; }
    
    public string BreweryComment { get; set; }

    public List<CommentData> Comments { get; set; }

}

public class CommentData
{
    public CommentData()
    {
        Comment = string.Empty;
    }
    
    public CommentData(Guid commentId, Guid userId, string comment)
    {
        this.CommentId = commentId;
        this.UserId = userId;
        this.Comment = comment;
    }

    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public string Comment { get; set; }

   
}
