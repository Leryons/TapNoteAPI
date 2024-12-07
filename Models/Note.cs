namespace TapNoteAPI.Models;

[FirestoreData]
public class Note
{
    [FirestoreProperty]
    public string Title { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Content { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Author { get; set; } = string.Empty;
}