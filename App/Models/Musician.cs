using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class Musician
{
    [Key]
    public int IdMusician { get; set; }

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    [MaxLength(20)]
    public string Nickname { get; set; }
    
    public virtual ICollection<MusicianTrack> MusicianTracks { get; set; }
}