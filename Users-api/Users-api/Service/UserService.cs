using Users_api.Dto;
using Users_api.Models;
using Users_api.Repository;

namespace Users_api.Service
{
    public class UserService : ICRUD<UserDTO, UserInsertDTO, UserUpdateDTO>
    {
         private IRepository<User> _repository;
        public UserService([FromKeyedServices("UserRepository")] IRepository<User> repository) 
        { 
            _repository = repository;
        }

        public async Task<IEnumerable<UserDTO>> Get()
        {
            var users = await _repository.Get();
            return users.Select(x => new UserDTO
            {
                UserId = x.UserId,
                Name = x.Name,
                Email = x.Email
            });
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _repository.GetById(id);
            return user != null ? new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email
            } 
            : 
            null;
        }

        public async Task<UserDTO> Create(UserInsertDTO insertDTO)
        {
            var newUser = new User
            {
                Name = insertDTO.Name,
                Email = insertDTO.Email
            };

            var users = await Get();

            foreach (var user in users)
            {
                if (user.Name == newUser.Name)
                {
                    throw new InvalidOperationException("Un usuario con el mismo email ya existe");
                }
            }

            await _repository.Create(newUser);
            await _repository.Save();

            return new UserDTO {UserId = newUser.UserId ,Name = newUser.Name, Email = newUser.Email };
        }

        public async Task<UserDTO> Delete(int id)
        {
            var userToDelete = await _repository.GetById(id);

            if (userToDelete == null) return null;

            _repository.Delete(userToDelete);
            await _repository.Save();

            return new UserDTO { UserId = userToDelete.UserId, Name = userToDelete.Name, Email = userToDelete.Email };
        }

        public async Task<UserDTO> Update(int id, UserUpdateDTO updateDTO)
        {
            var userToUpdate = await _repository.GetById(id);
            
            if (userToUpdate == null)
            {
                return null;
            }

            userToUpdate = new User
            {
                Name = updateDTO.Name,
                Email = updateDTO.Email
            };

            _repository.Update(userToUpdate);
            await _repository.Save();

            return new UserDTO { UserId = userToUpdate.UserId, Name = userToUpdate.Name, Email = userToUpdate.Email };
        }
    }
}
