using MyPlayMarket.Core.IServices;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;
using MyPlayMarket.Core.IRepository;
using Microsoft.Extensions.Logging;
using Humanizer;

namespace MyPlayMarket.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        public UserService(IUserRepository repository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            ILogger<UserService> logger)
        {
            _userRepository = repository;
            _passwordHasher= passwordHasher;
            _jwtProvider = jwtProvider;            
            _logger = logger;
        }

        public async Task<string> Register(UserRegisterDTO userDTO)
        {
            try
            {
                var hashedPassord = _passwordHasher.Generate(userDTO.Password);
                var user = new User(userDTO.Name, userDTO.Surname, userDTO.UserName, hashedPassord, userDTO.Email,"User");
                return await _userRepository.UserAddAsync(user);
            }
            catch(Exception ex) 
            {
                _logger.LogError($"{ex.Message}");
                return "Unexpected error";
            }
        }
        public async Task<string> Login(LoginUserDTO loginUserDTO)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(loginUserDTO.Email);
                var result = _passwordHasher.Verify(loginUserDTO.Password, user.PasswordHash);
                if (user == null && result == false)
                {
                    throw new Exception();
                }

                var token = _jwtProvider.GenerateToken(user);
                return token;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to login, user doesn't exist or invalid password");
                return null;
            }
           
        }  
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}
