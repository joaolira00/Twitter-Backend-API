namespace TwitterAPI.Entities
{
    public class Posts
    {
        public int authorId { get; set ;}
        public string? postContent { get ; set ;}
        public DateTime postTime { get; set;}
        public string? postAuthor { get; set ;}

        public void CreateNewPost(Users id)
        {
            
        }

    }
}