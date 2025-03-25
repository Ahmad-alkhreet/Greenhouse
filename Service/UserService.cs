using Domain;
using DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _userRepository.GetUserByNameAsync(name);
        }

        public async Task RegisterUserAsync(string name, UserRole role)
        {
            var user = new User(0, name, role); // Assuming Id is auto-incremented
            await _userRepository.AddAsync(user);
        }
    }
}
