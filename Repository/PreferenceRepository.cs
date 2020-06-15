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
