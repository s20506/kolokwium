using Microsoft.EntityFrameworkCore;

namespace App.Models;

public class MainDbContext : DbContext
{
    protected MainDbContext()
    {
    }

    public MainDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Musician> Musicians { get; set; }
    public DbSet<MusicianTrack> MusicianTracks { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<MusicLabel> MusicLabels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Musician>(p =>
        {
            p.HasData(new Musician()
                    {IdMusician = 1, FirstName = "Jan", LastName = "Kowalski", Nickname = "Janek"},
                new Musician()
                    {IdMusician = 2, FirstName = "Anna", LastName = "Nowak", Nickname = "Anka"});
        });
        modelBuilder.Entity<MusicLabel>(p =>
        {
            p.HasData(new MusicLabel() {IdMusicLabel = 1, Name = "Nazwa ..."},
                new MusicLabel() {IdMusicLabel = 2, Name = "Label 2"});
        });
        modelBuilder.Entity<Album>(p =>
        {
            p.HasData(
                new Album()
                {
                    IdAlbum = 1, AlbumName = "Nazwa albumu", PublishDate = DateTime.Parse("2022-01-01"),
                    IdMusicLabel = 1
                },
                new Album()
                {
                    IdAlbum = 2, AlbumName = "Drugi album", PublishDate = DateTime.Parse("2021-01-01"), IdMusicLabel = 2
                }
            );
        });
        modelBuilder.Entity<Track>(p =>
        {
            p.HasData(new Track()
                {IdTrack = 1, TrackName = "Traaack", Duration = 20, IdMusicAlbum = 1});
        });
        modelBuilder.Entity<MusicianTrack>(p =>
        {
            p.HasKey(e => new {e.IdTrack, e.IdMusician});
            p.HasData(new MusicianTrack()
            {
                IdMusician = 1, IdTrack = 1
            });
        });
    }
}