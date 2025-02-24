namespace FUForum.ViewModels.Contents;

public class CommentVM
{
    public int Id { get; set; }

    public string Content { get; set; }
    
    public int KnowledgeBaseId { get; set; }
    
    public string OwnerUserId { get; set; }

    public DateTime CreateDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}