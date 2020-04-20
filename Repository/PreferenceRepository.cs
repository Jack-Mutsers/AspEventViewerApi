using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class PreferenceRepository : RepositoryBase<Preference>, IPreferenceRepository
    {
        public PreferenceRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_Preference(Preference preference)
        {
            Create(preference);
        }

        public void Delete_Preference(Preference preference)
        {
            Delete(preference);
        }

        public IEnumerable<Preference> Get_Preference_by_user(int user_id)
        {
            return FindByCondition(p => p.user_id == user_id);
        }

        public void Update_Preference(Preference preference)
        {
            Update(preference);
        }
    }
}
