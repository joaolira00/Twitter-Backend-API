using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAPI.Entities;
using TwitterAPI.Database;

[Route("api/UsersData")]
[ApiController]

public class UsersController : ControllerBase
{
    private readonly UserDatabase _userContext;
    public UsersController(UserDatabase context)
    {
        _userContext = context;
    }

    //Fetches all users.
    [HttpGet]
    public ActionResult<IEnumerable<Users>> GetAll()
    {
        var users = _userContext.UsersList?.ToList();
        if (users is null)
        {
            return NotFound("Users not found.");
        } 
        return users;
    }

    //Fetch one specific user.
    [HttpGet("{id:int}", Name = "GetUsername")]
    public ActionResult<Users> GetById(int id)
    {
        var users = _userContext.UsersList?.FirstOrDefault(p=> p.UserId == id);
        if (users is null)
        {
            return NotFound("User Id not found.");
        }
        return users;
    }

    //Create a user with username, Id and password
    [HttpPost]
    public ActionResult Post(Users username)
    {
        if (username is null)
        {
            return BadRequest("Username can't be null.");
        }

        return new CreatedAtRouteResult("GetUsername",
        new { username.UserId, username.Username, username.Password });

    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Users username)
    {
        var previousUsername = _userContext.UsersList?.SingleOrDefault(d => d.UserId == id);

        if (id != username.UserId)
        {
            return BadRequest("Id not found...");
        }

        previousUsername?.UpdateAll(username.Username, username.Password);

        return Ok(username);
    }

    //Password Change
    [HttpPatch("{id:int}")]
    public ActionResult Patch(int id, Users userPassword)
    {
        var previousPassword = _userContext.UsersList?.SingleOrDefault(d => d.UserId == id);

        if (id != userPassword.UserId)
        {
            return BadRequest("Id not found...");
        }

        previousPassword?.UpdatePassword(userPassword.Password);

        return Ok(userPassword);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var username = _userContext.UsersList?.FirstOrDefault(p=> p.UserId == id);

        if (username is null)
        {
            return NotFound("Username not found...");
        }
        _userContext.UsersList?.Remove(username);

        return Ok(username);

    }

}

[Route("api/PostsData")]
[ApiController]

public class PostController : ControllerBase
{
    private readonly PostsDatabase? _postsContext;
    public PostController(PostsDatabase context)
    {
        _postsContext = context;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Posts>> GetAllPosts()
    {
        var posts = _postsContext?.PostsList?.ToList();

        if (posts is null)
        {
            return NotFound("No posts found...");
        }

        return posts;

    }

    [HttpGet("{id:int}", Name = "GetUserPosts")]
    public ActionResult<IEnumerable<Posts>> GetByUsername(int id, [FromQuery] Users _userId)
    {

        var userPostsId = _postsContext?.PostsList?.Where(p=> p.authorId == id);
        //var auth = _postsContext?.PostsList?.SingleOrDefault();
        
        
        if(userPostsId is null)
        {
            return NotFound("User id can't be empty.");
        }


        return userPostsId.ToList();
    }

    [HttpPost]
    public ActionResult Post(Posts userPost, [FromQuery] Users userId)
    {
        
        if (userPost is null)
        {
            return BadRequest("Your post can't be empty!!!");
        }

        if (userPost.authorId == userId.UserId)
        {
             _postsContext?.PostsList?.Add(userPost);

        return new CreatedAtRouteResult("GetPosts",
        new { author = userPost.postAuthor , userPost.postContent, userPost.authorId });
        }

       else
       {
            return NotFound("User Id doesn't exist.");
       }
        
    }
    
}