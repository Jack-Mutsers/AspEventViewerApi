using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace Contracts.Logic
{
    public interface IPreferenceLogic
    {
        IEnumerable<PreferenceDto> GetPreferenceByUser(int user_id);
        PreferenceDto GetById(int preference_id);
        bool Create(PreferenceForCreationDto preferenceForCreation);
        bool Delete(int id);
    }
}
