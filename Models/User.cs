namespace TapNoteAPI.Models;

[FirestoreData]
public class User
{
    [FirestoreProperty]
    [MaxLength(25)]
    [Required]
    public string Username { get; set; } = string.Empty;

    [FirestoreProperty]
    [MaxLength(25)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty]
    [MaxLength(25)]
    [Required]
    public string LastName { get; set; } = string.Empty;

    [FirestoreProperty]
    [MaxLength(25)]
    [Required]
    public string Password { get; set; } = string.Empty;
}