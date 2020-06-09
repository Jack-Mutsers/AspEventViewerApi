using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class PreferenceRepository : IPreferenceRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Preference> _preferences;

        public PreferenceRepository() 
        {
            _preferences = collection.preferences;
        }

        public void CreatePreference(Preference preference)
        {
            Create(preference);
        }

        public void DeletePreference(Preference preference)
        {
            Delete(preference);
        }

        public Preference GetById(int preference_id)
        {
            return FindByCondition(p => p.id == preference_id).FirstOrDefault();
        }

        public IEnumerable<Preference> GetPreferenceByUser(int user_id)
        {
            return FindByCondition(p => p.user_id == user_id);
        }

        public void UpdatePreference(Preference preference)
        {
            Preference preference1 = _preferences.FirstOrDefault(p => p.id == preference.id);
            if (preference1 != null)
                preference1 = preference;
        }
    }
}
