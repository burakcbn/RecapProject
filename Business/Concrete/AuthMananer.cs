using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWt;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthMananer : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthMananer(IUserService userService,ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetOperationClaim(user);
            return new SuccesDataResult<AccessToken>(_tokenHelper.CreateToken(user, claims.Data));
        }

        public IDataResult<User> Login(UserForLogin userForLogin)
        {
            var result = _userService.GetByEmail(userForLogin.Email);
            if (result.Data==null)
            {
                return new ErrorDataResult<User>(null, "Kullanıcı kaydı bulunamadı");
            }
            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password,result.Data.PasswordHash,result.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(null,"Şifre hatalı");
            }
            return new SuccesDataResult<User>(result.Data,"Kullanıcı giriş yaptı");
        }

        public IDataResult<User> Register(UserForRegister userForRegister)
        {
            var result = UserExist(userForRegister.Email);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(null, "Kullanıcı Kaydı bulunmaktadır ");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                Email = userForRegister.Email,
                FirstName = userForRegister.FirstName,
                LastName = userForRegister.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccesDataResult<User>(user, "Kullanıcı kaydı gerçekleştirildi");
        }

        public IResult UserExist(string email)
        {
            var result = _userService.GetByEmail(email);
            if (result.Data!=null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
