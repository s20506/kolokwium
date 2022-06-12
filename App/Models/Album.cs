using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class Album
{
    [Key]
    public int IdAlbum { get; set; }

    [Required]
    [MaxLength(30)]
    public string AlbumName { get; set; }
    
    [Required]
    public DateTime PublishDate { get; set; }
    
    public int IdMusicLabel { get; set; }
    
    public virtual ICollection<Track> Tracks { get; set; }
    
    [ForeignKey("IdMusicLabel")]
    public virtual MusicLabel MusicLabel { get; set; }
}