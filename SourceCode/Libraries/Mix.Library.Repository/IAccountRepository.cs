using Mix.Library.Entity.Database;
using System;

namespace Mix.Library.Repository
{
    public interface IAccountRepository
    {
        Account Get(Guid id);

        void AddAccount(Account model);
    }
}