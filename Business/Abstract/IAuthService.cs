using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.JWt;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegister userForRegister);
        IDataResult<User> Login(UserForLogin userForLogin);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult UserExist(string email);
    }
}
