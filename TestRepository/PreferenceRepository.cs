using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class PreferenceRepository : RepositoryBase, IPreferenceRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Preference> _preferences;

        public PreferenceRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _preferences = collection.preferences;
        }

        public void Create(Preference preference)
        {
            _preferences.Add(preference);
        }

        public void Delete(Preference preference)
        {
            _preferences.Remove(preference);
        }

        public Preference GetById(int preference_id)
        {
            return _preferences.Where(p => p.id == preference_id).FirstOrDefault();
        }

        public IEnumerable<Preference> GetPreferenceByUser(int user_id)
        {
            return _preferences.Where(p => p.user_id == user_id);
        }

        public void Update(Preference preference)
        {
            Preference preference1 = _preferences.FirstOrDefault(p => p.id == preference.id);
            if (preference1 != null)
                preference1 = preference;
        }

    }
}
