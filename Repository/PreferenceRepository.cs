using Contracts.Repository;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class PreferenceRepository : RepositoryBase<Preference>, IPreferenceRepository
    {
        public PreferenceRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void DeleteByUser(int user_id)
        {
            IEnumerable<Preference> preferences = FindByCondition(p => p.user_id == user_id).ToList();
            foreach (Preference preference in preferences)
            {
                Delete(preference);
            }
        }

        public Preference GetById(int preference_id)
        {
            return FindByCondition(p => p.id == preference_id).FirstOrDefault();
        }

        public IEnumerable<Preference> GetPreferenceByUser(int user_id)
        {
            return FindByCondition(p => p.user_id == user_id);
        }

    }
}
