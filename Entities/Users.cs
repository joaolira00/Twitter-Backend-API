namespace TwitterAPI.Entities
{

    public class Users
    {
        public int UserId { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }

        public void UpdateAll(string? _username, string? _password)
        {
            Username = _username;
            Password = _password;

        }

        public void UpdatePassword(string? _password)
        {
            Password = _password;
        }


    }

    
    
}