using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;
using NLog;
using MyPlayMarket.Core.IRepository;

namespace MyPlayMarket.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repository,ILogger logger)
        {
            _userRepository = repository;
            _logger = logger;
        }

        public async Task Register(UserRegisterDTO userDTO)
        {
            try
            {
                var hashedPassord = Generate(userDTO.Password);
                var user = new User(Guid.NewGuid(), userDTO.Name, userDTO.Surname, userDTO.UserName, hashedPassord, userDTO.Email, "User");
                //await _userRepository.Add(user);
            }
            catch(Exception ex) 
            {

            }
        }
        public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public Task Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
