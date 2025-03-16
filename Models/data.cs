namespace BlogEngine.Models;
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsPublished { get; set; } = false; // New field for publishing
    public List<Comment> Comments { get; set; } = new List<Comment>();
}
public class Comment
{
    public int Id { get; set; }
    public int BlogPostId { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsApproved { get; set; } = false;

    // Navigation property to BlogPost
    public BlogPost BlogPost { get; set; }
}