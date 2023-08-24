namespace TwitterAPI.Entities
{
    public class Posts
    {
        public int authorId { get; set ;}
        public string? postContent { get ; set ;}
        public int postID { get ; set ;}
        public string? postAuthor { get; set ;}

        public void EditPost(string? _postContent)
        {
            postContent = _postContent;
        }

    }
}