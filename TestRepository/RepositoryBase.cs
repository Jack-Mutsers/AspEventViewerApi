using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestRepository
{
    public abstract class RepositoryBase
    {
        public RepositoryContext RepositoryContext { get; private set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public void Save()
        {

        }
    }
}