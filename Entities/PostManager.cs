using TwitterAPI.Entities;

namespace TwitterAPI.PostsManager
{
    public class PostsManager
{
    private readonly List<Users> _usersList;
    private readonly List<Posts> _postsList;

    public PostsManager(List<Users> usersList, List<Posts> postsList)
    {
        _usersList = usersList;
        _postsList = postsList;
    }

    public bool UserExists(int userId)
    {
        return _usersList.Any(u => u.UserId == userId);
    }

    public void AddPost(Posts post)
    {
        _postsList.Add(post);
    }
}
}
