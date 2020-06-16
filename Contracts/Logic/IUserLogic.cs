using Entities.DataTransferObjects;

namespace Contracts.Logic
{
    public interface IUserLogic
    {
        UserDto GetUserByLogin(string username, string password);
        UserDto GetById(int User_id);
        UserDto Create(UserForCreationDto userForCreation);
        bool Update(UserForUpdateDto userForUpdate);
        bool Delete(int id);
    }
}
