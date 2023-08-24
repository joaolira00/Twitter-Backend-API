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
    public ActionResult<IEnumerable<Users>> GetAllUsers()
    {
        var users = _userContext.UsersList?.ToList();
        if (users is null)
        {
            return NotFound("Users not found.");
        } 
        
        return users;
    }

    //Fetch one specific user.
    [HttpGet("{id:int}", Name="GetUserInformation")]
    public ActionResult<Users> GetById(int id)
    {
        var users = _userContext.UsersList?.FirstOrDefault(u=> u.UserId == id);
        if (users is null)
        {
            return NotFound("User Id not found.");
        }
        
        return users;
    }

    //Create a user with username, Id and password
    [HttpPost]
    public ActionResult Post(Users userInformation)
    {

        if (userInformation is not null)
        {
            if (_userContext.UsersList is null)
            {
                _userContext.UsersList = new List<Users>();
            }

            _userContext.UsersList?.Add(userInformation);

            return new CreatedAtRouteResult("GetUserInformation",
            new { userInformation.Username, userInformation.Password });
        }

        return BadRequest("Username can't be null.");

    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Users userInformation)
    {
        var previousUsername = _userContext.UsersList?.SingleOrDefault(u => u.UserId == id);

        if (id != userInformation.UserId)
        {
            return BadRequest("Id not found...");
        }

        previousUsername?.UpdateAll(userInformation.Username, userInformation.Password);

        return Ok(userInformation);
    }

    //Password Change
    [HttpPatch("{id:int}")]
    public ActionResult Patch(int id, Users userPassword)
    {
        var previousPassword = _userContext.UsersList?.SingleOrDefault(u => u.UserId == id);

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
        var UserToDelete = _userContext.UsersList?.FirstOrDefault(u=> u.UserId == id);

        if (UserToDelete is null)
        {
            return NotFound("Username not found...");
        }
        _userContext.UsersList?.Remove(UserToDelete);

        return Ok(UserToDelete);

    }

}
