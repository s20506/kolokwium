namespace App.Models;

public class MusicianDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nickname { get; set; }
    public ICollection<TrackListItemDto> Tracks { get; set; }
}