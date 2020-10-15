using Grpc.Core;
using Mix.Library.Entity.Databases.Accounts;
using Mix.Library.Entity.Protos;
using Mix.Library.Repository.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Mix.Library.Entity.Protos.Accounts;

namespace Mix.Grpc.Api.Services
{
    public class AccountService : AccountsBase
    {
        private readonly IUserRepository userRepository;

        public AccountService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var user = await userRepository.GetUserAsync(r => r.UserName == request.Username || r.Email == request.Username);
            if (user == null)
            {
                throw new Exception("用户不存在");
            }
            Tokens tokens = CreateToken(user);
            
            return await Task.FromResult(new LoginResponse { Tokens = tokens });
        }

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            return base.Register(request, context);
        }

        private Tokens CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim (ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim (ClaimTypes.Name, user.UserName ?? ""),
                new Claim (ClaimTypes.Email, user.Email ?? ""),
            };

            return new Tokens { AccessToken = GenerateToken(), RefreshToken = GenerateToken() };
        }

        private string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}