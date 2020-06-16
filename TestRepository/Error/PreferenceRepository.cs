using Contracts.Repository;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TestRepository.succes;

namespace TestRepository.Error
{
    public class PreferenceRepository : RepositoryBase, IPreferenceRepository
    {
        public PreferenceRepository(RepositoryContext repositoryContext = null) : base(repositoryContext) { }

        public void Create(Preference preference)
        {
            throw new Exception("this is a Unit Test for Create");
        }

        public void Delete(Preference preference)
        {
            throw new Exception("this is a Unit Test for Delete");
        }

        public Preference GetById(int preference_id)
        {
            throw new Exception("this is a Unit Test for GetById");
        }

        public IEnumerable<Preference> GetPreferenceByUser(int user_id)
        {
            throw new Exception("this is a Unit Test for GetPreferenceByUser");
        }

        public void Update(Preference preference)
        {
            throw new Exception("this is a Unit Test for Update");
        }

        public void DeleteByUser(int user_id)
        {
            throw new Exception("this is a Unit Test for DeleteByUser");
        }
    }
}
