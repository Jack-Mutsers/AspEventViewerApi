using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> Get_All_artists();
        Artist Get_Artist_By_Id(int artist_id);
        void Create_Artist(Artist artist);
        void Update_Artist(Artist artist);
        void Delete_Artist(Artist artist);
    }
}
