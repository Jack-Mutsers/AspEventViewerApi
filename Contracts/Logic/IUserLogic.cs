using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IUserLogic
    {
        UserDto GetUserByLogin(string username, string password);
        UserDto GetById(int User_id);
        bool Create(UserForCreationDto userForCreation);
        bool Update(UserForUpdateDto userForUpdate);
        bool Delete(int id);
    }
}
