namespace TapNoteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{

    FirestoreDb db = FirestoreDb.Create(Environment.GetEnvironmentVariable("PROJECT_ID"));

    [HttpGet] //Get all notes
    public async Task<ActionResult<List<Note>>> GetNotes()
    {
        var noteRef = db.Collection("Notes");

        QuerySnapshot snapshots = await noteRef.GetSnapshotAsync();

        List<Note> notes = snapshots.Documents.Select(x => x.ConvertTo<Note>()).ToList();

        return Ok(notes);
    }

    [HttpPost] //Send note
    public async Task<ActionResult<Note>> PostNotes(string noteTile, string noteContent, string noteAuthor)
    {
        var newNote = new Note()
        {
            Title = noteTile,
            Content = noteContent,
            Author = noteAuthor
        };

        DocumentReference df = db.Collection("Notes").Document($"{newNote.Author}_{DateTime.Now.ToString("f")}");

        await df.SetAsync(newNote);

        return Ok(newNote);
    }
}