using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chinook.Domain.Extensions;
using Chinook.Domain.Responses;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public async Task<IEnumerable<AlbumResponse>> GetAllAlbumAsync(CancellationToken ct = default)
        {
            var albums = await _albumRepository.GetAllAsync(ct);
            return albums.ConvertAll();
        }

        public async Task<AlbumResponse> GetAlbumByIdAsync(int id, CancellationToken ct = default)
        {
            var albumViewModel = (await _albumRepository.GetByIdAsync(id, ct)).Convert;
            albumViewModel.ArtistName = (await _artistRepository.GetByIdAsync(albumViewModel.ArtistId, ct)).Name;
            return albumViewModel;
        }

        public async Task<IEnumerable<AlbumResponse>> GetAlbumByArtistIdAsync(int id, 
            CancellationToken ct = default)
        {
            var albums = await _albumRepository.GetByArtistIdAsync(id, ct);
            return albums.ConvertAll();
        }

        public async Task<AlbumResponse> AddAlbumAsync(AlbumResponse newAlbumViewModel,
            CancellationToken ct = default)
        {
            var album = new Album
            {
                Title = newAlbumViewModel.Title,
                ArtistId = newAlbumViewModel.ArtistId
            };

            album = await _albumRepository.AddAsync(album, ct);
            newAlbumViewModel.AlbumId = album.AlbumId;
            return newAlbumViewModel;
        }

        public async Task<bool> UpdateAlbumAsync(AlbumResponse albumViewModel,
            CancellationToken ct = default)
        {
            var album = await _albumRepository.GetByIdAsync(albumViewModel.AlbumId, ct);

            if (album is null) return false;
            album.AlbumId = albumViewModel.AlbumId;
            album.Title = albumViewModel.Title;
            album.ArtistId = albumViewModel.ArtistId;

            return await _albumRepository.UpdateAsync(album, ct);
        }

        public Task<bool> DeleteAlbumAsync(int id, CancellationToken ct = default) 
            => _albumRepository.DeleteAsync(id, ct);
    }
}