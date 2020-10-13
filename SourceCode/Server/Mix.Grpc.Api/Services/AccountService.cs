using Grpc.Core;
using Mix.Library.Entity.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mix.Library.Entity.Protos.Accounts;

namespace Mix.Grpc.Api.Services
{
    public class AccountService : AccountsBase
    {
        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            return Task.FromResult(new LoginResponse
            {
                Code = 200,
                Message = "Success"
            });
        }
    }
}