namespace TapNoteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    FirestoreDb db = FirestoreDb.Create(Environment.GetEnvironmentVariable("PROJECT_ID"));

    [HttpPost] //Sign up
    public async Task<ActionResult<User>> SignUp(string username, string name, string lastname, string password)
    {
        var newUser = new User()
        {
            Username = username,
            Name = name,
            LastName = lastname,
            Password = password
        };

        Query query = db.Collection("Users").WhereEqualTo("Username", username);
        QuerySnapshot snapshots = await query.GetSnapshotAsync();

        if (snapshots.Documents.Count > 0)
        {
            DocumentSnapshot userSp = snapshots.Documents[0];
            User user = userSp.ConvertTo<User>();

            if (user.Username == username)
            {
                return BadRequest("User already used.");
            }
        }

        DocumentReference df = db.Collection("Users").Document($"{newUser.Username}");

        await df.SetAsync(newUser);

        return Ok(newUser);
    }

    [HttpGet] //Login
    public async Task<ActionResult<User>> Login(string username, string password)
    {
        Query query = db.Collection("Users").WhereEqualTo("Username", username);
        QuerySnapshot snapshots = await query.GetSnapshotAsync();

        if (snapshots.Documents.Count > 0)
        {
            DocumentSnapshot userSp = snapshots.Documents[0];
            User user = userSp.ConvertTo<User>();

            if (user.Username == username && user.Password == password)
            {
                return Ok(user);
            }
        }

        return BadRequest("Invalid Username or Password.");
    }
}