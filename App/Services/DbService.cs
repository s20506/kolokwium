using System.Net;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_5.Services;

public class DbService : IDbService
{
    private readonly MainDbContext _dbContext;

    public DbService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<MusicianDto?> GetMusician(int idMusician)
    {
        return await _dbContext.Musicians.Select(e => new MusicianDto()
            {
                Id = e.IdMusician,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Nickname = e.Nickname,
                Tracks = e.MusicianTracks.Select(mt => mt)
                    .Where(mt => mt.IdMusician == e.IdMusician)
                    .Select(mt => new TrackListItemDto()
                    {
                        Id = mt.Track.IdTrack,
                        Name = mt.Track.TrackName,
                        Duration = mt.Track.Duration
                    })
                    .OrderBy(t => t.Duration)
                    .ToList()
            })
            .Where(e => e.Id == idMusician)
            .SingleOrDefaultAsync();
    }
    
    public async Task DeleteMusician(int idMusician)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var res = await _dbContext.Musicians.Select(e => new 
                    {
                        Id = e.IdMusician,
                        TracksCount = e.MusicianTracks
                            .Select(mt => mt)
                            .Where(mt => mt.IdMusician == e.IdMusician)
                            .Select(mt => new
                            {
                                IdMusicAlbum = mt.Track.IdMusicAlbum
                            })
                            .Count(mt => mt.IdMusicAlbum != null)
                    })
                    .Where(e => e.Id == idMusician)
                    .SingleOrDefaultAsync();

                if (res == null) throw new HttpRequestException("Musician not found!", null, HttpStatusCode.NotFound);
                if (res.TracksCount > 0) throw new HttpRequestException("Musician's tracks are already in album!");
                
                _dbContext.Musicians.Remove(new Musician()
                {
                    IdMusician = idMusician
                });
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync();
                throw exception;
            }
        }
    }
}