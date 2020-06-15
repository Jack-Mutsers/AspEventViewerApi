using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IPreferenceLogic
    {
        IEnumerable<PreferenceDto> GetPreferenceByUser(int user_id);
        PreferenceDto GetById(int preference_id);
        bool Create(PreferenceForCreationDto preferenceForCreation);
        bool Delete(int id);
    }
}
