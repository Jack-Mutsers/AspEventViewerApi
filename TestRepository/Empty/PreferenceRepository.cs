using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestRepository.succes;

namespace TestRepository.Empty
{
    public class PreferenceRepository : RepositoryBase, IPreferenceRepository
    {
        public PreferenceRepository(RepositoryContext repositoryContext = null) : base(repositoryContext) { }

        public void Create(Preference preference) { }

        public void Delete(Preference preference) { }

        public Preference GetById(int preference_id)
        {
            return new Preference();
        }

        public IEnumerable<Preference> GetPreferenceByUser(int user_id)
        {
            return new List<Preference>();
        }

        public void Update(Preference preference) { }

        public void DeleteByUser(int user_id){}
    }
}
