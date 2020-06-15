using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Logic
{
    public interface IArtistLogic
    {
        IEnumerable<ArtistDto> GetAll();
        IEnumerable<ArtistDto> GetArtistsByEventDate(int event_date_id);

        ArtistDto GetArtistById(int id);

        ArtistDto GetArtistByIdWithDetails(int id);

        bool CreateArtist(ArtistForCreationDto artistForCreation);

        bool UpdateArtist(ArtistForUpdateDto artistForUpdate);

        bool DeleteArtist(int id);

    }
}
