using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUniversalRepository<T>
    {
        RepositoryContext RepositoryContext { get; }
        void Create(T model);
        void Update(T model);
        void Delete(T model);
        void Save();
    }
}
