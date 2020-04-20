using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IPreferenceRepository
    {
        IEnumerable<Preference> Get_Preference_by_user(int user_id);
        void Create_Preference(Preference preference);
        void Update_Preference(Preference preference);
        void Delete_Preference(Preference preference);
    }
}
