using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAPI.Entities;
using TwitterAPI.Database;

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
            return BadRequest("User id can't be empty.");
        }

        if (id == _userId.UserId)
        {
            return userPostsId.ToList();
        }

        else
        {
            return NotFound("User not found.");
        }
        
    }

    [HttpPost]
    public ActionResult Post(Posts userPost)
    {
        
        if (userPost is null)
        {
            return BadRequest("Your post can't be empty!!!");
        }

        _postsContext?.PostsList?.Add(userPost);

        return new CreatedAtRouteResult("GetPosts",
        new { author = userPost.postAuthor , userPost.postContent, userPost.authorId });
        
    }

    [HttpPatch("{id:int}")]

    public ActionResult Patch(int id, Posts postContent)
    {
        var previousContent = _postsContext?.PostsList?.SingleOrDefault(p => p.postID == id);

        if (id != postContent.postID)
        {
            return BadRequest("Post not found...");
        }

        previousContent?.EditPost(postContent.postContent);

        return Ok(postContent);
    }

    [HttpDelete("{id:int}")]

    public ActionResult DeletePost(int id)
    {
        var PostToDelete = _postsContext?.PostsList?.FirstOrDefault(p => p.postID == id);

        if (PostToDelete is null)
        {
            return NotFound("Post not found...");
        }

        _postsContext?.PostsList?.Remove(PostToDelete);

        return Ok(PostToDelete);
    }
    
}