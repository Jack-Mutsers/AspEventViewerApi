using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IPreferenceRepository : IUniversalRepository<Preference>
    {
        IEnumerable<Preference> GetPreferenceByUser(int user_id);
        Preference GetById(int preference_id);
    }
}
