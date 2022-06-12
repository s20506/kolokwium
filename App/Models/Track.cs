using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class Track
{
    [Key]
    public int IdTrack { get; set; }

    [Required]
    [MaxLength(20)]
    public string TrackName { get; set; }
    
    [Required]
    public float Duration { get; set; }
    
    public int? IdMusicAlbum { get; set; }
    
    public virtual ICollection<MusicianTrack> MusicianTracks { get; set; }
    
    [ForeignKey("IdMusicAlbum")]
    public virtual Album? Album { get; set; }
}