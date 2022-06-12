using App.Models;

namespace APBD_5.Services;

public interface IDbService
{
    Task<MusicianDto?> GetMusician(int idMusician);
}