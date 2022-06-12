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
}