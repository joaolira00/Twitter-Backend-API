using TwitterAPI.Entities;

namespace TwitterAPI.Database
{
    public class UserDatabase
    {
        public List<Users>? UsersList { get; set; }

        public UserDatabase()
        {
            UsersList = new List<Users>();
            UsersList.Add(new Users() { UserId = 0, Username = "Lira", Password = "123"});
            UsersList.Add(new Users() { UserId = 1, Username = "Fabo", Password = "321"});
            UsersList.Add(new Users() { UserId = 2, Username = "Gabriel", Password = "456"});
            UsersList.Add(new Users() { UserId = 3, Username = "Stenio", Password = "654"});
            UsersList.Add(new Users() { UserId = 4, Username = "Freitas", Password = "789"});

        }


    }

    public class PostsDatabase
    {
        public List<Posts>? PostsList { get; set;}

        public PostsDatabase()
        {
            PostsList = new List<Posts>();
            PostsList.Add(new Posts() { postAuthor = "Lira", postContent = "Good morning to y'all!!!" });
        }
    }
}