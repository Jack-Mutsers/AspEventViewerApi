using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IPreferenceRepository
    {
        IEnumerable<Preference> GetPreferenceByUser(int user_id);
        Preference GetById(int preference_id);
        void CreatePreference(Preference preference);
        void UpdatePreference(Preference preference);
        void DeletePreference(Preference preference);
    }
}
