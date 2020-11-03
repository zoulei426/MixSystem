using Grpc.Core;
using Mix.Library.Entities.Databases.Accounts;
using Mix.Library.Entities.Protos;
using Mix.Library.Repositories.Accounts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Mix.Library.Entities.Protos.Accounts;

namespace Mix.Grpc.Api.Services
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountService : AccountsBase
    {
        private readonly IUserRepository userRepository;

        public AccountService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var user = await userRepository.GetUserAsync(r => r.Username == request.Username || r.Email == request.Username);
            if (user == null)
            {
                throw new Exception("用户不存在");
            }
            Tokens tokens = CreateToken(user);

            return await Task.FromResult(new LoginResponse { Tokens = tokens });
        }

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };
            var res = userRepository.InsertAsync(user);
            if (res == null)
            {
                return Task.FromResult(new RegisterResponse
                {
                    Response = new UnifyResponseDto
                    {
                        Code = ErrorCode.Error,
                        Message = "注册失败"
                    }
                });
            }
            return Task.FromResult(new RegisterResponse
            {
                Response = new UnifyResponseDto
                {
                    Code = ErrorCode.Success,
                    Message = "注册成功"
                }
            });
        }

        private Tokens CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.Name, user.Username ?? ""),
                new Claim (ClaimTypes.Email, user.Email ?? ""),
            };

            return new Tokens { AccessToken = GenerateToken(), RefreshToken = GenerateToken() };
        }

        private string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}