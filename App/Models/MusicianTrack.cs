using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class MusicianTrack
{
    public int IdTrack { get; set; }
    public int IdMusician { get; set; }
    
    [ForeignKey("IdTrack")]
    public virtual Track Track { get; set; }
    
    [ForeignKey("IdMusician")]
    public virtual Musician Musician { get; set; }
}