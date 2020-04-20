using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class PreferenceRepository : RepositoryBase<Preference>, IPreferenceRepository
    {
        public PreferenceRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

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
            Update(preference);
        }
    }
}
