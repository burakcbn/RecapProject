using Core.Entities.Concrete;
using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IDataResult<User> GetByEmail(string email);
        IDataResult<List<OperationClaim>> GetOperationClaim(User user);
    }
}
