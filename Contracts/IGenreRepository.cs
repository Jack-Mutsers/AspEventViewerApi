using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> Get_All_Genres();
        Genre Get_Genre_By_Id(int genre_id);
    }
}
